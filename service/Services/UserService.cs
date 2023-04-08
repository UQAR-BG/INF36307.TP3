using INF36307.TP3.Models;
using INF36307.TP3.Producers;
using INF36307.TP3.Repositories.Interfaces;
using INF36307.TP3.Utils;

namespace INF36307.TP3.Services;

public class UserService : IUserService
{
    private readonly IProducer _producer;
    private readonly IEtudiantRepository _repository;
    private readonly JsonUtils<User> _jsonUtils;

    public UserService(IProducer producer, IEtudiantRepository repository)
    {
        _producer = producer;
        _repository = repository;
        _jsonUtils = new JsonUtils<User>();
    }
    
    public void PublishUserWithEmail(string name)
    {
        var user = _jsonUtils.Deserialize(name);
        if (user != null)
        {
            Etudiant etudiant = _repository.First(user.Nom);
            
            string serializedUser = JsonUtils<Etudiant>.Serialize(etudiant);
            _producer.Produce(serializedUser);
        }
        else
        {
            _producer.Produce(name);
        }
    }
}