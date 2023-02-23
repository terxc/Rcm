using Genl.Framework.Mediating;
using Genl.Framework;
using Rcm.Services.Users.Core;
using Rcm.Services.Users.Core.Commands;
using Rcm.Services.Users.Core.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore();

var app = builder.Build();
app.UseCore();

app.MapGet("/", (AppOptions options) => options.AppInfo);
app.MediatePost<SignUpCommand>("sign-up");
app.MediatePost<SignInCommand>("sign-in", StatusCodes.Status200OK);
app.MediateGet<GetUserQuery>("users/{id}", policyNames: new[] { "UsersView" });
app.MediatePut<UpdateUserCommand>("users/{id}", policyNames: new[] { "UsersEdit" });

app.Run();