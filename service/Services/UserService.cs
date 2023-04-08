using INF36307.TP3.Producers;

namespace INF36307.TP3.Services;

public class UserService : IUserService
{
    private readonly IProducer _producer;

    public UserService(IProducer producer)
    {
        _producer = producer;
    }
    
    public void PublishUserWithEmail(string name)
    {
        _producer.Produce(name);
    }
}