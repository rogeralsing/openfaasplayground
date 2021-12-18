using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenFaaS.Hosting;


void Setup(WebApplicationBuilder builder)
{
    // add your services to the container
}

void Configure(WebApplication app)
{
    // configure the HTTP request pipeline
    app.MapPost("/", () => new { Message = "Hello" });
}

Runner.Run( args, Setup, Configure );
