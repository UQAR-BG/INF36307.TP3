using Confluent.Kafka;
using INF36307.TP3.Configuration;
using Microsoft.Extensions.Options;

namespace INF36307.TP3;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly KafkaOptions _options;

    public Worker(ILogger<Worker> logger, IOptions<KafkaOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ConsumerConfig config = new ConsumerConfig
        {
            GroupId = _options.GroupId,
            BootstrapServers = _options.BootstrapServers,
            AutoOffsetReset = _options.AutoOffsetReset
        };

        ProducerConfig producerConfig = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            BatchSize = _options.BatchSize,
            LingerMs = _options.LingerMs,
            CompressionType = _options.CompressionType,
            Acks = _options.Acks
        };

        using var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build();
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        consumerBuilder.Subscribe(_options.ConsumerTopic);

        try
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumerBuilder.Consume(stoppingToken);
                    Console.WriteLine($"Consumed message '{result.Message.Value}' at: '{result.TopicPartitionOffset}'.");
                    
                    producer.Produce(
                        _options.ProducerTopic, 
                        new Message<Null, string> { Value=result.Message.Value },
                        (deliveryReport) =>
                        {
                            Console.WriteLine($"Produced message '{deliveryReport.Message.Value}' at: '{deliveryReport.TopicPartitionOffset}'.");
                        });
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumerBuilder.Close();
        }
    }
}