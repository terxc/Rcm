using Genl.Framework;
using Rcm.Services.Notifications.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore();

var app = builder.Build();
app.UseCore();

app.MapGet("/", (AppOptions options) => options.AppInfo);

app.Run();
