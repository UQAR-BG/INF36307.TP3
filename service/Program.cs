using INF36307.TP3;
using INF36307.TP3.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();