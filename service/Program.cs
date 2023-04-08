using INF36307.TP3;
using INF36307.TP3.Configuration;
using INF36307.TP3.Consumers;
using INF36307.TP3.Producers;
using INF36307.TP3.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));
        services.AddScoped<IProducer, KafkaProducer>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IConsumer, KafkaConsumer>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();