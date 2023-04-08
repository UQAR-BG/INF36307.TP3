using Confluent.Kafka;
using INF36307.TP3.Configuration;
using Microsoft.Extensions.Options;

namespace INF36307.TP3.Producers;

public class KafkaProducer: IProducer
{
    private readonly ILogger<Worker> _logger;
    private readonly KafkaOptions _options;
    private readonly IProducer<Null, string> _producer;
    
    public KafkaProducer(ILogger<Worker> logger, IOptions<KafkaOptions> options)
    {
        _logger = logger;
        _options = options.Value;
        ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers
            // BatchSize = _options.BatchSize,
            // LingerMs = _options.LingerMs,
            // CompressionType = _options.CompressionType,
            // Acks = _options.Acks
        };
        
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }
    
    public void Produce(string message)
    {
        _producer.Produce(
            _options.ProducerTopic, 
            new Message<Null, string> { Value=message },
            (deliveryReport) =>
            {
                _logger.LogInformation($"Produced message '{deliveryReport.Message.Value}' at: '{deliveryReport.TopicPartitionOffset}'.");
            });
    }
}