using Genl.App;
using Rcm.Services.Navigation.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore(builder.Configuration);

var app = builder.Build();
app.UseCore();

app.MapGet("/", (AppOptions options) => options.AppInfo);

app.Run();