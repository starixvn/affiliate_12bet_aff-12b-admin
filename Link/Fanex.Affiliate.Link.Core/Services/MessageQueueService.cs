using EasyNetQ;
using EasyNetQ.Topology;
using Fanex.Affiliate.Link.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Link.Core.Services;

public sealed class MessageQueueService : IMessageQueueService
{
    private const int TimeDelayBeforeRetrySeconds = 10;
    private const int MaxRetryAttempts = 5;

    private readonly IBus bus;
    private readonly ILogService logService;
    private Exchange exchange;
    private Queue queue;

    public MessageQueueService(IBus bus, ILogService logService)
    {
        this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
        this.logService = logService ?? throw new ArgumentNullException(nameof(logService));
        Initialize();
    }

    private void Initialize()
    {
        exchange = bus.Advanced.ExchangeDeclare($"{Constants.QueueIdentify}.exchange", ExchangeType.Direct);
        queue = bus.Advanced.QueueDeclare($"{Constants.QueueIdentify}.queue", c =>
        {
            c.AsDurable(true);
            c.AsExclusive(false);
            c.AsAutoDelete(false);
            c.WithArgument("x-queue-type", "quorum");
        });
        bus.Advanced.Bind(exchange, queue, $"{Constants.QueueIdentify}.routing");

        logService.LogFileMessage("Exchange and queue already exist, using existing infrastructure");
    }

    public async Task PublishAsync<T>(T model)
    {
        int retryCount = 0;
        while (retryCount <= MaxRetryAttempts)
        {
            try
            {
                await bus.Advanced.PublishAsync(
                    exchange,
                    routingKey: $"{Constants.QueueIdentify}.routing",
                    mandatory: false,
                    new Message<T>(model)
                ).ConfigureAwait(false);

                logService.LogFileMessage($"Published model {JsonConvert.SerializeObject(model)} successfully!");
                break;
            }
            catch (Exception ex)
            {
                retryCount++;
                await logService.SentryErrorAsync(
                    $"Error publishing object: {JsonConvert.SerializeObject(model)}. Retrying {retryCount}/{MaxRetryAttempts}.",
                    ex
                ).ConfigureAwait(false);

                if (retryCount > MaxRetryAttempts)
                {
                    throw;
                }

                await Task.Delay(TimeSpan.FromSeconds(TimeDelayBeforeRetrySeconds)).ConfigureAwait(false);
            }
        }
    }
}
