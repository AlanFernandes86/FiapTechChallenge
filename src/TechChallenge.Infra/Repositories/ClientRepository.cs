using Dapper;
using System.Data;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;
using TechChallenge.Infra.Provider;

namespace TechChallenge.Infra.Repositories;

public class ClientRepository: IClientRepository
{
    private readonly IDbConnection _dbConnection;

    public ClientRepository(IDatabaseProvider databaseProvider)
    {
        _dbConnection = databaseProvider.DbConnection;
    }

    public async Task<Client> GetClient(long cpf)
    {
        var sql = @"SELECT [cpf]
                      ,[name]
                      ,[email]
                      ,[updated_at]
                      ,[created_at]
                  FROM [TechChallenge].[dbo].[client]
                  WHERE [cpf] = @cpf";

        var parameters = new DynamicParameters();
        parameters.Add("cpf", cpf);

        return await _dbConnection.QueryFirstAsync<Client>(sql, parameters);    
    }

    public async Task<bool> PutClient(Client client)
    {
        var sql = @"MERGE INTO [dbo].[client] AS target
                    USING (
                    VALUES
                        (@cpf
                        ,@name
                        ,@email
                        ,GETDATE()
                        ,GETDATE()
                        )
                    ) AS source 
                        ([cpf]
                       ,[name]
                       ,[email]
                       ,[updated_at]
                       ,[created_at])
                    ON target.[cpf] = source.[cpf]
                    WHEN MATCHED THEN
                        UPDATE SET
                        target.[name] = source.[name],
                        target.[email] = source.[email],
                        target.[updated_at] = source.[updated_at]
                    WHEN NOT MATCHED THEN
                        INSERT
                           ([cpf]
                           ,[name]
                           ,[email]
                           ,[updated_at]
                           ,[created_at])
                        VALUES
                           (source.[cpf]
                           ,source.[name]
                           ,source.[email]
                           ,source.[updated_at]
                           ,source.[created_at]);";

        var parameters = new DynamicParameters();
        parameters.Add("cpf", client.Cpf);
        parameters.Add("name", client.Name);
        parameters.Add("email", client.Email);

        return await _dbConnection.ExecuteAsync(sql, parameters) > 0;
    }
}
