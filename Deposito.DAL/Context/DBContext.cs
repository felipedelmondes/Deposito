using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Deposito.DAL.Context;

public class DBContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    
    public DBContext(IConfiguration configuration)
    {
        _configuration = configuration;
        this._connectionString = _configuration.GetConnectionString("PostgresDb");
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

}