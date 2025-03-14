using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using SimpleCRM.Backoffice.WebApp.API;
using SimpleCRM.Backoffice.WebApp.Filters;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddControllersWithViews()
    .Services
    .AddScoped<ISimpleCRMApi, SimpleCRMApi>()
    .AddScoped(typeof(SimpleCRMAuthorizeFilter))
    .AddHttpClient()
    .AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();  

app.UseRouting();

// app.UseAuthorization();
// app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();