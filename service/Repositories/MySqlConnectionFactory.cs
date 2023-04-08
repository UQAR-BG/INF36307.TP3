using System.Data;
using INF36307.TP3.Repositories.Interfaces;
using MySqlConnector;

namespace INF36307.TP3.Repositories;

public class MySqlConnectionFactory : IConnectionFactory
{
    private readonly MySqlConnection _connection;

    public MySqlConnectionFactory(MySqlConnection connection)
    {
        _connection = connection;
    }
    
    public MySqlConnection GetConnection()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        return _connection;
    }
}