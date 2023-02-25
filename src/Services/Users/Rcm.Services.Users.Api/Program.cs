using Genl.App;
using Genl.Framework.Mediating;
using Rcm.Services.Users.Core;
using Rcm.Services.Users.Core.Constants;
using Rcm.Services.Users.Core.CQRS;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore(builder.Configuration);

var app = builder.Build();
app.UseCore();

app.MapGet("/", (AppOptions options) => options.AppInfo);
app.MediatePost<SignUpCommand>("sign-up");
app.MediatePost<SignInCommand>("sign-in", StatusCodes.Status200OK);
app.MediateGet<GetUserQuery>("users/{id}", policyNames: new[] { Policy.UsersView });
app.MediatePut<UpdateUserCommand>("users/{id}", policyNames: new[] { Policy.UsersEdit });

app.Run();