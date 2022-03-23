using AspNetCoreHealthChecker;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//var healthSection = builder.Configuration.GetSection(nameof(AspNetCoreHealtChecker.Config.Health));
//builder.Services.Configure<AspNetCoreHealtChecker.Config.Health>(healthSection);

// builder.Configuration.AddJsonFile("Probes.json", optional: false, reloadOnChange: true);
builder.ConfigureHealthCheck();


var app = builder.Build();


var h = app.Services.GetService<IOptions<AspNetCoreHealthChecker.Config.HealthCheck>>();

app.UseAspNetHealthChecks();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
