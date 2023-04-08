using INF36307.TP3.Models;
using INF36307.TP3.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace INF36307.TP3.Repositories;

public class EtudiantRepositoryProxy : IEtudiantRepository
{
    private readonly IEtudiantRepository _repository;
    private readonly IEtudiantRepository _cache;

    public EtudiantRepositoryProxy(IConnectionFactory connectionFactory, IMemoryCache cache)
    {
        _repository = new EtudiantRepository(connectionFactory);
        _cache = new EtudiantCache(cache);

        IEnumerable<Etudiant> etudiants = _repository.All();
        _cache.AddRange(etudiants);
    }
    
    public Etudiant First(string username)
    {
        Etudiant etudiant = _cache.First(username);
        if (etudiant == null)
        {
            etudiant = _repository.First(username);
            _cache.Add(etudiant);
        }

        return etudiant;
    }

    public IEnumerable<Etudiant> All()
    {
        throw new NotImplementedException();
    }

    public Etudiant Add(Etudiant etudiant)
    {
        throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<Etudiant> etudiants)
    {
        throw new NotImplementedException();
    }
}