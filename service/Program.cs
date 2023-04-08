using INF36307.TP3;
using INF36307.TP3.Configuration;
using INF36307.TP3.Consumers;
using INF36307.TP3.Producers;
using INF36307.TP3.Repositories;
using INF36307.TP3.Repositories.Interfaces;
using INF36307.TP3.Services;
using MySqlConnector;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddTransient<MySqlConnection>(_ =>
            new MySqlConnection(configuration.GetConnectionString("Default")));
        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));
        services.AddScoped<IProducer, KafkaProducer>();
        services.AddScoped<IEtudiantRepository, EtudiantRepositoryProxy>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IConsumer, KafkaConsumer>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();