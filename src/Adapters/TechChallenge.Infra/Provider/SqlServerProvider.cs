using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using TechChallenge.Infra.Provider;

namespace TechChallenge.Infra.Context;

public class SqlServerProvider : IDatabaseProvider
{
    private readonly string? _connectionString;

    public SqlServerProvider(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqlServer");
    }

    public IDbConnection DbConnection => new SqlConnection(_connectionString);
}
