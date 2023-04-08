using System.Collections;
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
        IHostEnvironment env = hostContext.HostingEnvironment;
        IConfiguration config = hostContext.Configuration;
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

        builder.AddEnvironmentVariables("DOTNET_");

        services.AddMemoryCache();
        
        services.AddTransient<MySqlConnection>(_ =>
            new MySqlConnection(Environment.GetEnvironmentVariable("DOTNET_ConnectionStrings__DefaultConnection")));
        services.Configure<KafkaOptions>(config.GetSection("Kafka"));
        services.AddSingleton<IConnectionFactory, MySqlConnectionFactory>();
        services.AddScoped<IProducer, KafkaProducer>();
        services.AddScoped<IEtudiantRepository, EtudiantRepositoryProxy>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IConsumer, KafkaConsumer>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

// configuration.GetConnectionString("Default")