namespace INF36307.TP3.Consumers;

public interface IConsumer
{
    void Listen(CancellationToken stoppingToken);
}