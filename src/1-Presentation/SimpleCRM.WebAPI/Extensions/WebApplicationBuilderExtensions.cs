using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SimpleCRM.Application.Admin.Contracts.Services;
using SimpleCRM.Application.Admin.Profiles;
using SimpleCRM.Application.Admin.Services;
using SimpleCRM.Application.Admin.Validators;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Application.Attendant.Profiles;
using SimpleCRM.Application.Attendant.Services;
using SimpleCRM.Application.Attendant.Validators;
using SimpleCRM.Application.Common.Validators;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;
using SimpleCRM.Domain.Providers;
using SimpleCRM.Infra;
using SimpleCRM.Infra.MongoDB;
using SimpleCRM.WebAPI.Handlers;

namespace SimpleCRM.WebAPI.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddSimpleCRMControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation(fluentValidation =>
        {
            fluentValidation.DisableDataAnnotationsValidation = true;
        });

        // admin
        builder.Services.AddValidatorsFromAssemblyContaining<InsertUserRQValidator>();
        // attendant
        builder.Services.AddValidatorsFromAssemblyContaining<LoginRQValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CustomerRegisterRQValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<InteractionStartRQValidator>();

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var validationMessageRS = new ValidationRS();

                    foreach (var model in c.ModelState)
                    {
                        var errors = model.Value.Errors;
                        
                        if (errors.Count <= 0)
                            continue;

                        foreach (var error in errors)
                            validationMessageRS.AddValidation(model.Key, error.ErrorMessage);
                    }

                    return new BadRequestObjectResult(validationMessageRS);
                };
            });
        
        return builder;
    }
    
    public static WebApplicationBuilder AddSimpleCRMLogs(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc
            .ReadFrom.Configuration(ctx.Configuration)
        );
        builder.Services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.ResponsePropertiesAndHeaders |
                                    HttpLoggingFields.ResponseBody |
                                    HttpLoggingFields.RequestPropertiesAndHeaders |
                                    HttpLoggingFields.RequestBody;
        });

        return builder;
    }
    
    public static WebApplicationBuilder AddSimpleCRMAutoMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(UserProfile));
        builder.Services.AddAutoMapper(typeof(CustomerProfile));

        return builder;
    }

    public static WebApplicationBuilder AddSimpleCRMDependencyInjections(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSingleton<ExceptionHandler>()
            .AddSingleton<IDbMapper, MongoDbMapper>()
            // providers
            .AddScoped<IDbProvider<User>, MongoDbProvider<User>>()
            .AddScoped<IDbProvider<Customer>, MongoDbProvider<Customer>>()
            .AddScoped<IDbProvider<Interaction>, MongoDbProvider<Interaction>>()
            // repositories
            .AddScoped<IRepository<User>, Repository<User>>()
            .AddScoped<IRepository<Customer>, Repository<Customer>>()
            .AddScoped<IRepository<Interaction>, Repository<Interaction>>()
            // services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<IInteractionService, InteractionService>()
            // managers
            .AddScoped<UserManager>()
            .AddScoped<TokenManager>()
            .AddScoped<CustomerManager>()
            .AddScoped<InteractionManager>();

        return builder;
    }

    public static void AddSimpleCRMAuthentication(this WebApplicationBuilder builder)
    {
        var secret = builder.Configuration.GetSection("Authentication").GetValue<string>("Secret");

        if (string.IsNullOrEmpty(secret))
            throw new Exception("Authentication.Secret not defined in AppSettings");
        
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
    
    public static WebApplicationBuilder AddSimpleCRMSwagger(this WebApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        return builder;
    }
}