using Microsoft.AspNetCore.Builder;
using OpenFaaS.Hosting;

void Setup(WebApplicationBuilder builder)
{
    // add your services to the container
}

void Configure(WebApplication app)
{
    // configure the HTTP request pipeline
    app.MapPost("/", (string body) => new { Message = "Hello4" + body });
    app.MapPost("/test", (string body) => new { Message = "test4" + body });
}

Runner.Run(args, Setup, Configure);