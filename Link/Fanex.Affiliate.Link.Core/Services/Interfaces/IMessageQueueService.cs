namespace Fanex.Affiliate.Link.Core.Services.Interfaces;

public interface IMessageQueueService
{
    Task PublishAsync<T>(T model);
}
