using Microsoft.AspNetCore.Builder;
using Rcm.Services.Users.Api;
using Rcm.Services.Users.Core;
using Rcm.Shared.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCore();

        var app = builder.Build();
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.MapEndpoints();

        app.Run();
    }
}