using INF36307.TP3.Models;
using INF36307.TP3.Repositories.Interfaces;
using MySqlConnector;

namespace INF36307.TP3.Repositories;

public class EtudiantRepository: IEtudiantRepository
{
    private readonly MySqlConnection _connection;

    public EtudiantRepository(MySqlConnection connection)
    {
        _connection = connection;
    }
    
    public Etudiant First(string username)
    {
        _connection.Open();

        using var command = new MySqlCommand($"SELECT * FROM etudiants WHERE nom=?nom;", _connection);
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
        
        _connection.Close();
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