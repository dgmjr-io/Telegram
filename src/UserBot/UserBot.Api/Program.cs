using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;
using Telegram.UserBot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddTheWorks(
    @params =>
        @params
            .WithAddAzureAppConfig(true)
            .WithAzureKeyVaultConfigurator(kv => kv.SetCredential(new DefaultCredential())) //(kv => kv.)
            .Build()
);
builder.AddUserBot();

var app = builder.Build();

app.UseTheWorks();

app.Run();
