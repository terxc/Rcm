using MassTransit;
using Microsoft.Extensions.Logging;
using Rcm.Contracts.Users;

namespace Rcm.Services.Navigation.Core.Consumers;

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

public class UserSignedUpConsumerDefinition : ConsumerDefinition<UserSignedUpConsumer>
{
    public UserSignedUpConsumerDefinition()
    {
        ConcurrentMessageLimit = 8;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserSignedUpConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseDelayedRedelivery(r => r.Interval(5, 1000));
        endpointConfigurator.UseMessageRetry(r => r.Interval(5, 5000));
        endpointConfigurator.UseInMemoryOutbox();
    }
}