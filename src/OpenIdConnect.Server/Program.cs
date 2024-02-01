using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Telegram.OpenIdConnect.Json;
using Telegram.OpenIdConnect.Middleware;
using Telegram.OpenIdConnect.Services;
using Telegram.OpenIdConnect.Services.CodeService;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

// using Serilog;
// using Log = Serilog.Log;

// Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.AutoConfigure<Program>();
builder.Services.AddTransient(
    provider => provider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session
);
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.AutoConfigure();
app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials());
app.UseMiddleware<ClientSessionMiddleware>();
app.Run();
