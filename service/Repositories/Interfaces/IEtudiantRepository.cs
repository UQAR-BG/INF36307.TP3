using INF36307.TP3.Models;

namespace INF36307.TP3.Repositories.Interfaces;

public interface IEtudiantRepository
{
    Etudiant First(string username);
    IEnumerable<Etudiant> All();
    Etudiant Add(Etudiant etudiant);
    void AddRange(IEnumerable<Etudiant> etudiants);
}