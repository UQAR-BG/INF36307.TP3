using Confluent.Kafka;
using INF36307.TP3.Configuration;
using INF36307.TP3.Services;
using Microsoft.Extensions.Options;

namespace INF36307.TP3.Consumers;

public class KafkaConsumer : IConsumer
{
    private readonly ILogger<Worker> _logger;
    private readonly KafkaOptions _options;
    private readonly ConsumerConfig _config;
    private readonly IUserService _service;
    
    public KafkaConsumer(
        ILogger<Worker> logger, 
        IOptions<KafkaOptions> options, 
        IUserService service)
    {
        _service = service;
        _logger = logger;
        _options = options.Value;
        _config = new ConsumerConfig
        {
            GroupId = _options.GroupId,
            BootstrapServers = _options.BootstrapServers,
            AutoOffsetReset = _options.AutoOffsetReset
        };
    }
    
    public void Listen(CancellationToken stoppingToken)
    {
        using var consumerBuilder = new ConsumerBuilder<Ignore, string>(_config).Build();
        consumerBuilder.Subscribe(_options.ConsumerTopic);
        
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumerBuilder.Consume(stoppingToken);
                    _logger.LogInformation($"Consumed message '{result.Message.Value}' at: '{result.TopicPartitionOffset}'.");
                    
                    _service.PublishUserWithEmail(result.Message.Value);
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"Error occured: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumerBuilder.Close();
        }
    }
}