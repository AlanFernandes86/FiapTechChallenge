using System.Data;

namespace TechChallenge.Infra.Provider;

public interface IDatabaseProvider
{
    public IDbConnection DbConnection { get; }
}
