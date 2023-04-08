using INF36307.TP3.Models;
using INF36307.TP3.Repositories.Interfaces;

namespace INF36307.TP3.Repositories;

public class EtudiantCache : IEtudiantRepository
{
    public Etudiant First(string username)
    {
        return null;
    }

    public IEnumerable<Etudiant> All()
    {
        throw new NotImplementedException();
    }

    public Etudiant Add(Etudiant etudiant)
    {
        return etudiant;
    }
}