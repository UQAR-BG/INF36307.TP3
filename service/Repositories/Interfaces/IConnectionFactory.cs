using MySqlConnector;

namespace INF36307.TP3.Repositories.Interfaces;

public interface IConnectionFactory
{
    MySqlConnection GetConnection();
}