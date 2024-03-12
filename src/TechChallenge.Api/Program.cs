using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TechChallenge.Api;
using TechChallenge.Application.Hubs;
using TechChallenge.Domain.Options;
using TechChallenge.Infra;

string[] ApiVersions = { "v1" };
string AppTitle = "TechChallenge Fiap - Fase 2";
string AppDescription = "Aplicação desenvolvida como parte da entrega do TechChallenge da PosTech Fiap - Software Architeture";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc(options =>
{
    options.Conventions.Add(new ApiExplorerGroupPerVersionConvetion());
});

builder.Services.AddSwaggerGen(options =>
{
    foreach (var api in ApiVersions )
    {
        options.SwaggerDoc(api, new OpenApiInfo
        {
            Version = api,
            Title = AppTitle,
            Description = AppDescription
        });
    }
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddGatewaysAndUseCases();
builder.Services.AddValidators();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddSignalR();
builder.Services.Configure<OrderHubOptions>(builder.Configuration.GetSection(nameof(OrderHubOptions)));

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting().UseEndpoints(e =>
{
    e.MapControllers();
    e.MapHub<OrderHub>("/OrderHub");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = AppTitle;
        foreach (var version in ApiVersions)
        {
            c.SwaggerEndpoint("/swagger/" + version + "/swagger.json", version + " Docs");
        }
    });
}

app.Run();
