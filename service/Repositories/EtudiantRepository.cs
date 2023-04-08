using INF36307.TP3.Models;
using INF36307.TP3.Repositories.Interfaces;
using MySqlConnector;

namespace INF36307.TP3.Repositories;

public class EtudiantRepository: IEtudiantRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public EtudiantRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public Etudiant First(string username)
    {
        using var command = new MySqlCommand($"SELECT * FROM etudiants WHERE nom=?nom;", _connectionFactory.GetConnection());
        command.Parameters.Add(new MySqlParameter("nom", username));
        using var reader = command.ExecuteReader();

        Etudiant etudiant = new Etudiant();
        while (reader.Read())
        {
            etudiant = new Etudiant
            {
                Id = reader.GetInt32(0),
                Nom = reader.GetString(1),
                Email = reader.GetString(2)
            };
        }
        
        return etudiant;
    }

    public IEnumerable<Etudiant> All()
    {
        using var command = new MySqlCommand($"SELECT * FROM etudiants;", _connectionFactory.GetConnection());
        using var reader = command.ExecuteReader();

        List<Etudiant> etudiants = new List<Etudiant>();
        while (reader.Read())
        {
            etudiants.Add(new Etudiant
            {
                Id = reader.GetInt32(0),
                Nom = reader.GetString(1),
                Email = reader.GetString(2)
            });
        }
        
        return etudiants;
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