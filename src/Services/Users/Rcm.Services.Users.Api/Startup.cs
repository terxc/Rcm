using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rcm.Services.Users.Core;
using Rcm.Shared.Middlewares;

namespace Rcm.Services.Users.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly string _appName;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _appName = configuration["app:name"];
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCore();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseMiddleware<ErrorHandlerMiddleware>();

        //app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", context => context.Response.WriteAsync(_appName));
            endpoints.MapControllers();
        });
    }
}





































