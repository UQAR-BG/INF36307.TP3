using INF36307.TP3.Models;
using INF36307.TP3.Repositories.Interfaces;
using MySqlConnector;

namespace INF36307.TP3.Repositories;

public class EtudiantRepositoryProxy : IEtudiantRepository
{
    private readonly IEtudiantRepository _repository;
    private readonly IEtudiantRepository _cache;

    public EtudiantRepositoryProxy(MySqlConnection mySqlConnection)
    {
        _repository = new EtudiantRepository(mySqlConnection);
        _cache = new EtudiantCache();
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
}