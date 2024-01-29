using Dapper;
using System.Data;
using System.Transactions;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Ports.Repositories;
using TechChallenge.Infra.Provider;

namespace TechChallenge.Infra.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly IDbConnection _dbConnection;

    public OrderRepository(IDatabaseProvider databaseProvider)
    {
        _dbConnection = databaseProvider.DbConnection;
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus orderStatus)
    {
        var sql = @"SELECT o.[id]
                      ,o.[order_status_id] as StatusId
                      ,o.[client_cpf] as ClientCpf
                      ,o.[updated_at]
                      ,o.[created_at]
                      ,op.[id]
                      ,op.[product_id] as ProductId
                      ,op.[price] as Price
                      ,op.[quantity] as Quantity
                      ,p.[name] 
                  FROM [TechChallenge].[dbo].[order] o
                    LEFT JOIN
                        [TechChallenge].[dbo].[order_product] op ON o.id = op.order_id
                    LEFT JOIN
                        [TechChallenge].[dbo].[product] p ON op.product_id = p.id
                  WHERE o.[order_status_id] = @orderStatus";

        var parameters = new DynamicParameters();
        parameters.Add("orderStatus", orderStatus);

        try
        {
            var lookup = new Dictionary<int?, Order>();

            await _dbConnection.QueryAsync<Order, OrderProduct, Product, Order>(sql, (order, orderProduct, product) =>
            {
                if (!lookup.TryGetValue(order.Id, out Order dictOrder))
                {
                    lookup.Add(order.Id, dictOrder = order);                    
                }

                if (orderProduct is not null)
                {
                    orderProduct.ProductName = product.Name;
                    dictOrder.Products.Add(orderProduct);
                }

                return dictOrder;
            }, parameters, splitOn: "id,name");

            return lookup.Values;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<int> CreateOrder(Order order)
    {
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var sql = @"INSERT INTO [dbo].[order]
                           (
                            [order_status_id]
                           ,[client_cpf]
                           ,[updated_at]
                           ,[created_at]
                            )
                     OUTPUT INSERTED.id
                     VALUES
                           (
                            @status
                           ,@clientCpf
                           ,GETDATE()
                           ,GETDATE()
                            )";

            var parameters = new DynamicParameters();
            parameters.Add("status", OrderStatus.CREATED);
            parameters.Add("clientCpf", order.ClientCpf);

            try
            {
                var orderId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, parameters);

                foreach (var orderProduct in order.Products)
                {
                    await PutProductToOrder(orderProduct, orderId);
                }

                transactionScope.Complete();

                return orderId > 0 ? orderId : -1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }

    public async Task<int> PutProductToOrder(OrderProduct orderProduct, int orderId)
    {
        var sql = @"MERGE INTO [dbo].[order_product] AS target
                    USING (
                    VALUES
                        (@id
                        ,@productId
                        ,@orderId
                        ,@quantity
                        ,@price
                        ,GETDATE()
                        ,GETDATE()
                        )
                    ) AS source 
                        ([id]
                       ,[product_id]
                       ,[order_id]
                       ,[quantity]
                       ,[price] 
                       ,[updated_at]
                       ,[created_at])
                    ON target.[id] = source.[id]
                    WHEN MATCHED THEN
                        UPDATE SET
                        target.[quantity] = source.[quantity],
                        target.[price] = source.[price],
                        target.[updated_at] = source.[updated_at]
                    WHEN NOT MATCHED THEN
                        INSERT
                           (
                           [product_id]
                           ,[order_id]
                           ,[quantity]
                           ,[price] 
                           ,[updated_at]
                           ,[created_at])
                        VALUES
                           (
                           source.[product_id] 
                           ,source.[order_id]
                           ,source.[quantity]
                           ,source.[price] 
                           ,source.[updated_at]
                           ,source.[created_at])
                        OUTPUT INSERTED.id;";

        var parameters = new DynamicParameters();
        parameters.Add("id", orderProduct.Id);
        parameters.Add("productId", orderProduct.ProductId);
        parameters.Add("orderId", orderId);
        parameters.Add("quantity", orderProduct.Quantity);
        parameters.Add("price", orderProduct.Price);

        try
        {
            var orderProductId = await _dbConnection.QueryFirstAsync<int>(sql, parameters);
            return orderProductId > 0 ? orderProductId : -1;
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    //public async Task<int> RemoveProductToOrder(OrderProduct orderProduct, int OrderId)
    //{

    //}
}
