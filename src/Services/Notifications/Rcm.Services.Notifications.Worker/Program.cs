using Rcm.Services.Notifications.Core;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddCore(builder.Configuration);

var host = builder.Build();

host.Run();
