using Confluent.Kafka;

namespace INF36307.TP3.Configuration;

public class KafkaOptions
{
    public const string Kafka = "Kafka";

    public string GroupId { get; set; } = String.Empty;
    public string BootstrapServers { get; set; } = String.Empty;
    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;
    public int BatchSize { get; set; } = 1000000;
    public int LingerMs { get; set; } = 5;
    public CompressionType CompressionType { get; set; } = CompressionType.None;
    public Acks Acks { get; set; } = Acks.All;
    public string ConsumerTopic { get; set; } = String.Empty;
    public string ProducerTopic { get; set; } = String.Empty;
}