using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using DBFirstApproach.Models;

namespace DBFirstApproach.Context
{
    // Create a partial class to extend the scaffolded DbContext
    public partial class ApplicationDbContext
    {
        // Add DbSets for stored procedure results (these are NOT tables)
        public DbSet<ProductDetailsResult> ProductDetailsResults { get; set; }
        public DbSet<ProductFeaturesResult> ProductFeaturesResults { get; set; }
        public DbSet<ProductCommentsResult> ProductCommentsResults { get; set; }
        public DbSet<UserOrderHistoryResult> UserOrderHistoryResults { get; set; }

        // Configure the models in OnModelCreatingPartial
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // Configure SP result entities as keyless
            modelBuilder.Entity<ProductDetailsResult>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null); // Not mapped to any view/table
            });

            modelBuilder.Entity<ProductFeaturesResult>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<ProductCommentsResult>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<UserOrderHistoryResult>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            // Configure view entity
            modelBuilder.Entity<VwActiveProducts>(entity =>
            {
                entity.ToView("vw_ActiveProducts"); // Map to the actual database view
            });
        }

        // =============================================
        // STORED PROCEDURE METHODS
        // =============================================

        // Method 1: Execute SP and return single result
        public async Task<ProductDetailsResult> GetProductDetailsAsync(long productId, bool includeDeleted = false)
        {
            var productIdParam = new SqlParameter("@ProductId", productId);
            var includeDeletedParam = new SqlParameter("@IncludeDeleted", includeDeleted);

            var results = await ProductDetailsResults
                .FromSqlRaw("EXEC sp_GetProductDetails @ProductId, @IncludeDeleted",
                    productIdParam, includeDeletedParam)
                .AsNoTracking()
                .ToListAsync();

            return results.FirstOrDefault();
        }

        // Method 2: Execute SP and return list
        public async Task<List<UserOrderHistoryResult>> GetUserOrderHistoryAsync(
            long userId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var userIdParam = new SqlParameter("@UserId", userId);
            var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);

            var results = await UserOrderHistoryResults
                .FromSqlRaw(@"EXEC sp_GetUserOrderHistory @UserId, @PageNumber, @PageSize",
                    userIdParam, pageNumberParam, pageSizeParam)
                .AsNoTracking()
                .ToListAsync();

            return results;
        }

        // Method 3: Execute SP with output parameters
        public async Task<(string? SessionToken, DateTime? ExpiresAt)> AuthenticateUserAsync(string username)
        {
            var usernameParam = new SqlParameter("@Username", username);
            var tokenParam = new SqlParameter
            {
                ParameterName = "@Token",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 256,
                Direction = System.Data.ParameterDirection.Output
            };
            var roleNameParam = new SqlParameter
            {
                ParameterName = "@RoleName",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 128,
                Direction = System.Data.ParameterDirection.Output
            };
            var userIdParam = new SqlParameter
            {
                ParameterName = "@UserId",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };

            await Database.ExecuteSqlRawAsync(
                "EXEC sp_AuthenticateUser @Username, @Token OUTPUT, @RoleName OUTPUT, @UserId OUTPUT",
                usernameParam, tokenParam, roleNameParam, userIdParam);

            return (
                SessionToken: tokenParam.Value?.ToString(),
                ExpiresAt: DateTime.Now.AddHours(24) // Adjust based on your logic
            );
        }

        // Method 4: Execute SP without return value (INSERT/UPDATE/DELETE)
        public async Task<int> CreateOrderAsync(long userId, string productIds)
        {
            var userIdParam = new SqlParameter("@UserId", userId);
            var productIdsParam = new SqlParameter("@ProductIds", productIds);

            var rowsAffected = await Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateOrder @UserId, @ProductIds",
                userIdParam, productIdsParam);

            return rowsAffected;
        }

        // Method 5: Execute SP and return scalar value
        public async Task<int> SoftDeleteProductAsync(long productId, long? deletedBy = null)
        {
            var productIdParam = new SqlParameter("@ProductId", productId);
            var deletedByParam = new SqlParameter("@DeletedBy",
                deletedBy.HasValue ? (object)deletedBy.Value : DBNull.Value);

            var rowsAffected = await Database.ExecuteSqlRawAsync(
                "EXEC sp_SoftDeleteProduct @ProductId, @DeletedBy",
                productIdParam, deletedByParam);

            return rowsAffected;
        }

        // =============================================
        // SCALAR FUNCTION METHODS
        // =============================================

        // Register scalar functions
        [DbFunction("fn_GetProductFinalPrice", "dbo")]
        public decimal GetProductFinalPrice(long productId, long colorId, long sizeId)
        {
            throw new NotSupportedException("This method can only be called within LINQ queries");
        }

        [DbFunction("fn_GetUserTotalSpent", "dbo")]
        public decimal GetUserTotalSpent(long userId)
        {
            throw new NotSupportedException("This method can only be called within LINQ queries");
        }

        [DbFunction("fn_IsProductAvailable", "dbo")]
        public bool IsProductAvailable(long productId, long colorId, long sizeId)
        {
            throw new NotSupportedException("This method can only be called within LINQ queries");
        }

        [DbFunction("fn_GetProductCommentCount", "dbo")]
        public int GetProductCommentCount(long productId)
        {
            throw new NotSupportedException("This method can only be called within LINQ queries");
        }

        [DbFunction("fn_GetUserRoleName", "dbo")]
        public string GetUserRoleName(long userId)
        {
            throw new NotSupportedException("This method can only be called within LINQ queries");
        }
    }
}
