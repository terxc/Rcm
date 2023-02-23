using MassTransit;
using Microsoft.Extensions.Logging;
using Rcm.Contracts.Users;

namespace Rcm.Services.Notifications.Core.Consumers;

public class UserSignedUpConsumer : IConsumer<UserSignedUp>
{
    private readonly ILogger<UserSignedUpConsumer> _logger;

    public UserSignedUpConsumer(ILogger<UserSignedUpConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserSignedUp> context)
    {
        _logger.LogInformation($"UserSignedUp id: {context.Message.UserId}, email: {context.Message.Email}");
        return Task.CompletedTask;
    }
}