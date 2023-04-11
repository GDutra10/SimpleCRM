using SimpleCRM.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .AddSimpleCRMLogs()
    .AddSimpleCRMControllers()
    .AddSimpleCRMSwagger()
    .AddSimpleCRMAutoMappers()
    .AddSimpleCRMDependencyInjections()
    .AddSimpleCRMAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// add middlewares
app.UseSimpleCRMMiddlewares();

app.UseCors(policy => policy.AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseHttpLogging();
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();