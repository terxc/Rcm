using Rcm.Services.Users.Api;
using Rcm.Services.Users.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore();

var app = builder.Build();
app.UseCore();

app.MapEndpoints();

app.Run();