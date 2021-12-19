using Microsoft.AspNetCore.Builder;
using OpenFaaS.Hosting;

void Setup(WebApplicationBuilder builder)
{
    // add your services to the container
}

void Configure(WebApplication app)
{
    app.MapPost("/", (string body) => new { Message     = "Hello" + body });
    app.MapPost("/test", (string body) => new { Message = "test4" + body });
    app.MapGet("/get", () => "tada!");
}

Runner.Run(args, Setup, Configure);