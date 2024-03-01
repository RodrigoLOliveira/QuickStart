using System.Reflection;
using Template.Components;
using Template.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços
builder.Services.AddRazorAndAuthenticationServices()
                .ConfigureAuthentication()
                .AddApplicationDbContext(builder.Configuration)
                .AddEmailSender();

builder.Services.AddScopedServices("Template.Business");

var app = builder.Build();

// Configuração do pipeline HTTP
app.ConfigurePipeline(app.Environment);

app.Run();
