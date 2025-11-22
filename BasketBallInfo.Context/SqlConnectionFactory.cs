using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BasketBallInfo.Context;

public class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public SqlConnection Create()
    {
        return new SqlConnection(_connectionString);
    }
}
