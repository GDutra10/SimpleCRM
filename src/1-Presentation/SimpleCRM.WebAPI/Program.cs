using SimpleCRM.Domain.Providers;
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

// mapping database
var dbMapper = app.Services.GetService<IDbMapper>();
if (dbMapper is null)
    throw new ArgumentException("IDbMapper not defined!");
dbMapper.Map();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// add middlewares
app.UseSimpleCRMMiddlewares();

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();