using Dapper;
using System.Data;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;
using TechChallenge.Infra.Provider;

namespace TechChallenge.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnection _dbConnection;

    public ProductRepository(IDatabaseProvider databaseProvider)
    {
        _dbConnection = databaseProvider.DbConnection;
    }

    public async Task<IEnumerable<ProductCategory>> GetProductCategories()
    {
        var sql = @"SELECT [id]
                      ,[name]
                      ,[updated_at]
                      ,[created_at]
                  FROM [TechChallenge].[dbo].[product_category]";

        return await _dbConnection.QueryAsync<ProductCategory>(sql);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
    {
        var sql = @"SELECT [id]
                      ,[product_category_id] as CategoryId
                      ,[name]
                      ,[description]
                      ,[price]
                      ,[updated_at]
                      ,[created_at]
                  FROM [TechChallenge].[dbo].[product]
                  WHERE [product_category_id] = @categoryId";

        var parameters = new DynamicParameters();
        parameters.Add("categoryId", categoryId);

        return await _dbConnection.QueryAsync<Product>(sql, parameters);
    }

    public async Task<int> PutProduct(Product product)
    {
        var sql = @"MERGE INTO [dbo].[product] AS target
                    USING (
                    VALUES
                        (@id
                        ,@productCategoryId
                        ,@name
                        ,@description
                        ,@price
                        ,GETDATE()
                        ,GETDATE()
                        )
                    ) AS source 
                        ([id]
                       ,[product_category_id] 
                       ,[name]
                       ,[description]
                       ,[price] 
                       ,[updated_at]
                       ,[created_at])
                    ON target.[id] = source.[id]
                    WHEN MATCHED THEN
                        UPDATE SET
                        target.[product_category_id] = source.[product_category_id],
                        target.[name] = source.[name],
                        target.[description] = source.[description],
                        target.[price] = source.[price],
                        target.[updated_at] = source.[updated_at]
                    WHEN NOT MATCHED THEN
                        INSERT
                           (
                           [product_category_id] 
                           ,[name]
                           ,[description]
                           ,[price] 
                           ,[updated_at]
                           ,[created_at])
                        VALUES
                           (
                           source.[product_category_id] 
                           ,source.[name]
                           ,source.[description]
                           ,source.[price] 
                           ,source.[updated_at]
                           ,source.[created_at])
                        OUTPUT INSERTED.id;";

        var parameters = new DynamicParameters();
        parameters.Add("id", product.Id);
        parameters.Add("productCategoryId", product.CategoryId);
        parameters.Add("name", product.Name);
        parameters.Add("description", product.Description);
        parameters.Add("price", product.Price);

        var productId = await _dbConnection.QueryFirstAsync<int>(sql, parameters);
        return productId > 0 ? productId : -1;
    }

    public async Task<int> PutProductCategory(ProductCategory productCategory)
    {
        var sql = @"MERGE INTO [dbo].[product_category] AS target
                    USING (
                    VALUES
                        (@id
                        ,@name
                        ,GETDATE()
                        ,GETDATE()
                        )
                    ) AS source 
                        ([id]
                       ,[name]
                       ,[updated_at]
                       ,[created_at])
                    ON target.[id] = source.[id]
                    WHEN MATCHED THEN
                        UPDATE SET
                        target.[name] = source.[name],
                        target.[updated_at] = source.[updated_at]
                    WHEN NOT MATCHED THEN
                        INSERT
                           ([id]
                           ,[name]
                           ,[updated_at]
                           ,[created_at])
                        VALUES
                           (source.[id]
                           ,source.[name]
                           ,source.[updated_at]
                           ,source.[created_at])
                        OUTPUT INSERTED.id;";

        var parameters = new DynamicParameters();
        parameters.Add("id", productCategory.Id);
        parameters.Add("name", productCategory.Name);

        var productCategoryId = await _dbConnection.QueryFirstAsync<int>(sql, parameters);
        return productCategoryId > 0 ? productCategoryId : -1;
    }
}
