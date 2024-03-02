using Dapper;
using System.Data;
using System.Transactions;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Ports.Repositories;
using TechChallenge.Infra.Provider;

namespace TechChallenge.Infra.Repositories;

public class PaymentRepository: IPaymentRepository
{
    private readonly IDbConnection _dbConnection;

    public PaymentRepository(IDatabaseProvider databaseProvider)
    {
        _dbConnection = databaseProvider.DbConnection;
    }

    public async Task<int> SetPayment(Payment payment)
    {
        var sql = @"INSERT INTO [dbo].[payment]
                           ([order_id]
                           ,[value]
                           ,[method]
                           ,[updated_at]
                           ,[created_at])
                     OUTPUT INSERTED.id
                     VALUES
                           (
                            @order_id
                           ,@value
                           ,@method
                           ,GETDATE()
                           ,GETDATE()
                            )";

        var parameters = new DynamicParameters();
        parameters.Add("order_id", payment.OrderId);
        parameters.Add("value", payment.Value);
        parameters.Add("method", payment.Method);

        try
        {
            var paymentId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, parameters);

            return paymentId > 0 ? paymentId : -1;
        }
        catch (Exception e)
        {
            return -1;
        }
    }
}
