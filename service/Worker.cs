using Confluent.Kafka;

namespace INF36307.TP3;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ConsumerConfig config = new ConsumerConfig
        {
            GroupId = "users-consumer-group",
            BootstrapServers = "kafka-0.kafka.default.svc.cluster.local:9092,kafka-1.kafka.default.svc.cluster.local:9092,kafka-2.kafka.default.svc.cluster.local:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        ProducerConfig producerConfig = new ProducerConfig
        {
            BootstrapServers = "kafka-0.kafka.default.svc.cluster.local:9092,kafka-1.kafka.default.svc.cluster.local:9092,kafka-2.kafka.default.svc.cluster.local:9092",
            BatchSize = 10000000,
            LingerMs = 200,
            CompressionType = CompressionType.Lz4,
            Acks = Acks.All
        };

        using var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build();
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        consumerBuilder.Subscribe("users");

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
                        "notification", 
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