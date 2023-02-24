using Genl.App;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("yarp.json", false);

builder.Services.AddApp(builder.Configuration);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetRequiredSection("reverseProxy"));

var app = builder.Build();

app.MapGet("/", (AppOptions options) => options.AppInfo);

app.UseRouting();
app.MapReverseProxy();

app.Run();
