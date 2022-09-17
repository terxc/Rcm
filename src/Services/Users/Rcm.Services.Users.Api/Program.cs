using Microsoft.AspNetCore.Builder;
using Rcm.Services.Users.Api;
using Rcm.Services.Users.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCore();

        var app = builder.Build();
        app.UseCore();

        app.MapEndpoints();

        app.Run();
    }
}