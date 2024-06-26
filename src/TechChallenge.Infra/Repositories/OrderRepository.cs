﻿using Dapper;
using System.Data;
using System.Transactions;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Repositories;
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
        int[] status = new int[] { (int)orderStatus };

        if (orderStatus == OrderStatus.ACTIVE)
        {
            status = new int[] { (int)OrderStatus.RECEIVED, (int)OrderStatus.IN_PREPARATION, (int)OrderStatus.READY };
        }

        var sql = @"SELECT o.[id]
                      ,o.[order_status_id] as StatusId
                      ,o.[client_cpf] as ClientCpf
                      ,o.[client_name] as ClientName
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
                  WHERE o.[order_status_id] IN @orderStatus";

        var parameters = new DynamicParameters();
        parameters.Add("orderStatus", status);

        var lookup = new Dictionary<int?, Order>();

        await _dbConnection.QueryAsync<Order, ProductOnOrder, Product, Order>(sql, (order, orderProduct, product) =>
        {
            if (!lookup.TryGetValue(order.Id, out Order dictOrder))
            {
                lookup.Add(order.Id, dictOrder = order);                    
            }

            if (orderProduct is not null)
            {
                orderProduct.ProductName = product.Name;
                dictOrder.ProductsOnOrder.Add(orderProduct);
            }

            return dictOrder;
        }, parameters, splitOn: "id,name");

        return lookup.Values;
     }

    public async Task<Order?> GetOrdersById(int orderId)
    {
        var sql = @"SELECT o.[id]
                      ,o.[order_status_id] as StatusId
                      ,o.[client_cpf] as ClientCpf
                      ,o.[client_name] as ClientName
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
                  WHERE o.[id] = @orderId";

        var parameters = new DynamicParameters();
        parameters.Add("orderId", orderId);

        Order? lookup = null;

        await _dbConnection.QueryAsync<Order, ProductOnOrder, Product, Order>(sql, (order, orderProduct, product) =>
        {
            if (lookup is null)
            {
                lookup = order;
            }

            if (orderProduct is not null)
            {
                orderProduct.ProductName = product.Name;
                lookup.ProductsOnOrder.Add(orderProduct);
            }

            return order;
        }, parameters, splitOn: "id,name");

        return lookup;
    }

    public async Task<int> CreateOrder(Order order)
    {
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var sql = @"INSERT INTO [dbo].[order]
                           (
                            [order_status_id]
                           ,[client_cpf]
                           ,[client_name]
                           ,[updated_at]
                           ,[created_at]
                            )
                     OUTPUT INSERTED.id
                     VALUES
                           (
                            @status
                           ,@clientCpf
                           ,@clientName
                           ,GETDATE()
                           ,GETDATE()
                            )";

            var parameters = new DynamicParameters();
            parameters.Add("status", OrderStatus.CREATED);
            parameters.Add("clientCpf", order.ClientCpf);
            parameters.Add("clientName", order.ClientName);

            var orderId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, parameters);

            foreach (var orderProduct in order.ProductsOnOrder)
            {
                await PutProductToOrder(orderProduct, orderId);
            }

            transactionScope.Complete();

            return orderId > 0 ? orderId : -1;
        }
    }

    public async Task<int> PutProductToOrder(ProductOnOrder orderProduct, int orderId)
    {
        var sql = @"MERGE INTO [dbo].[order_product] AS target
                    USING (
                    VALUES
                        (
                         @productId
                        ,@orderId
                        ,@quantity
                        ,@price
                        ,GETDATE()
                        ,GETDATE()
                        )
                    ) AS source 
                        (
                        [product_id]
                       ,[order_id]
                       ,[quantity]
                       ,[price] 
                       ,[updated_at]
                       ,[created_at])
                    ON target.[product_id] = source.[product_id] AND target.[order_id] = source.[order_id]
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
        parameters.Add("productId", orderProduct.ProductId);
        parameters.Add("orderId", orderId);
        parameters.Add("quantity", orderProduct.Quantity);
        parameters.Add("price", orderProduct.Price);

        var orderProductId = await _dbConnection.QueryFirstAsync<int>(sql, parameters);
        return orderProductId > 0 ? orderProductId : -1;
    }

    public async Task<int> RemoveProductToOrder(int orderProductId)
    {
        var sql = @"DELETE FROM [dbo].[order_product]
                        OUTPUT DELETED.id
                        WHERE [id] = @orderProductId";


        var parameters = new DynamicParameters();
        parameters.Add("orderProductId", orderProductId);

        var orderId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, parameters);

        return orderId > 0 ? orderId : -1;
    }

    public async Task<int> UpdateOrderStatus(int orderId, OrderStatus orderStatus)
    {
        var sql = @"UPDATE [dbo].[order]
                       SET [order_status_id] = @orderStatus
                          ,[updated_at] = GETDATE()
                     OUTPUT INSERTED.id
                     WHERE [id] = @orderId";

        var parameters = new DynamicParameters();
        parameters.Add("orderId", orderId);
        parameters.Add("orderStatus", orderStatus);

        var updatedId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, parameters);

        return updatedId > 0 ? updatedId : -1;
    }
}
