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
app.MediatePost<SignUpCommand>("api/account/sign-up", StatusCodes.Status204NoContent);
app.MediatePost<SignInCommand>("api/account/sign-in");
app.MediateGet<GetUserQuery>("api/users/{id}", policyNames: new[] { "UsersView" });
app.MediatePut<UpdateUserCommand>("api/users/{id}", policyNames: new[] { "UsersEdit" });

app.Run();