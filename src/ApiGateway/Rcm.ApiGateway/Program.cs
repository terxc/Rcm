using Genl.Framework;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("yarp.json", false);

builder.Services.AddGenl();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetRequiredSection("reverseProxy"));

var app = builder.Build();

app.MapGet("/", (AppOptions options) => options.AppInfo);

app.UseRouting();
app.MapReverseProxy();

app.Run();
