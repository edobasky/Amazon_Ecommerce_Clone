using System.Runtime.CompilerServices;
using Npgsql;

namespace Discount.Extentions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;  
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Discount Db Migration Started.");
                    ApplyMigration(config);
                    logger.LogInformation("Discount Db Migration Completed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occured while migrating the database");
                    throw;
                }
            }
            return host;
        }

        private static void ApplyMigration(IConfiguration config)
        {
            var retry = 5;
            while (retry > 0)
            {
                try
                {
                    using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var cmd = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                    ProductName VARCHAR(200) NOT NULL,
                                    Description TEXT,
                                    Amount DECIMAL(8,2))";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"INSERT INTO Coupon (ProductName, Description, Amount)
                                VALUES
                                ('Adidas', 'Discount on Adidas sneakers', 100.00),
                                ('Puma', 'Puma sportswear promo', 25.50),
                                ('Nike', 'Nike running shoes discount', 50.75),
                                ('Reebok', 'Reebok fitness gear special offer', 45.00),
                                ('Under Armour', 'Under Armour performance wear discount', 84.20)
                                ";
                    cmd.ExecuteNonQuery();
                    
                    break;
                }
                catch (Exception)
                {
                    retry--;
                    if (retry == 0)
                    {
                        throw;
                    }
                    
                }
            }
        }
    }
}
