using Confluent.Kafka;
using INF36307.TP3.Configuration;
using Microsoft.Extensions.Options;

namespace INF36307.TP3.Producers;

public class KafkaProducer: IProducer
{
    private readonly ILogger<Worker> _logger;
    private readonly KafkaOptions _options;
    private readonly ProducerConfig _config;
    
    public KafkaProducer(ILogger<Worker> logger, IOptions<KafkaOptions> options)
    {
        _logger = logger;
        _options = options.Value;
        _config = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            BatchSize = _options.BatchSize,
            LingerMs = _options.LingerMs,
            CompressionType = _options.CompressionType,
            Acks = _options.Acks
        };
    }
    
    public void Produce(string message)
    {
        using var producer = new ProducerBuilder<Null, string>(_config).Build();
        
        producer.Produce(
            _options.ProducerTopic, 
            new Message<Null, string> { Value=message },
            (deliveryReport) =>
            {
                _logger.LogInformation($"Produced message '{deliveryReport.Message.Value}' at: '{deliveryReport.TopicPartitionOffset}'.");
            });
    }
}