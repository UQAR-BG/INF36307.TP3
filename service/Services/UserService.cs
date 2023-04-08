using INF36307.TP3.Models;
using INF36307.TP3.Producers;
using INF36307.TP3.Utils;

namespace INF36307.TP3.Services;

public class UserService : IUserService
{
    private readonly IProducer _producer;
    private readonly JsonUtils<User> _jsonUtils;

    public UserService(IProducer producer)
    {
        _producer = producer;
        _jsonUtils = new JsonUtils<User>();
    }
    
    public void PublishUserWithEmail(string name)
    {
        var user = _jsonUtils.Deserialize(name);
        if (user != null)
        {
            Notification notif = new Notification
            {
                Nom = user.Nom,
                Email = "test@gmail.com"
            };
            
            string serializedUser = JsonUtils<Notification>.Serialize(notif);
            _producer.Produce(serializedUser);
        }
        else
        {
            _producer.Produce(name);
        }
    }
}