using Microsoft.EntityFrameworkCore;
using CreativeCollab.Models; // Your DbContext namespace
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreativeCollab.Data
{
    public static class DataSeeder
    {
        public static async Task InitializeDatabaseAsync(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>(); // Use Program for logging context
                var context = services.GetRequiredService<ApplicationDbContext>();

                try
                {
                    logger.LogInformation("Applying migrations (if configured)...");
                    // Optional: context.Database.Migrate();

                    logger.LogInformation("Seeding database if necessary...");

                    // *** Only need to call SeedProductsAsync from here now ***
                    // It will handle the dependency on suppliers internally.
                    await SeedProductsAsync(context, logger);

                    // If you had other independent tables to seed, you could call their seed methods here.

                    logger.LogInformation("Database seeding process completed.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during database initialization.");
                    throw;
                }
            }
        }

        // SeedSuppliersAsync remains mostly the same, it just seeds suppliers if they don't exist.
        private static async Task SeedSuppliersAsync(ApplicationDbContext context, ILogger<Program> logger)
        {
            // Check if suppliers already exist
            if (await context.Suppliers.AnyAsync())
            {
                logger.LogInformation("Suppliers table already seeded.");
                return; // Already seeded
            }

            logger.LogInformation("Suppliers table is empty. Seeding from SQL file...");

            var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "SQLQuery-Suppliers.sql");

            if (!File.Exists(sqlFilePath))
            {
                logger.LogError($"Supplier seed script not found at: {sqlFilePath}");
                return;
            }

            try
            {
                string sqlScript = await File.ReadAllTextAsync(sqlFilePath);

                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    await context.Database.ExecuteSqlRawAsync(sqlScript);
                    await transaction.CommitAsync();
                }

                logger.LogInformation("Suppliers table seeded successfully from SQL file.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the Suppliers table from SQL file.");
                throw;
            }
        }

        // SeedProductsAsync now ensures suppliers are seeded first if needed.
        private static async Task SeedProductsAsync(ApplicationDbContext context, ILogger<Program> logger)
        {
            // Check if products already exist
            if (await context.Products.AnyAsync())
            {
                logger.LogInformation("Products table already seeded.");
                return; // Already seeded
            }

            logger.LogInformation("Products table is empty. Ensuring suppliers are seeded first...");

            // *** Call SeedSuppliersAsync here to handle the dependency ***
            // It will check internally if seeding is needed.
            await SeedSuppliersAsync(context, logger);

            // Add a safety check after attempting supplier seed
            if (!await context.Suppliers.AnyAsync())
            {
                logger.LogError("Suppliers table is still empty after attempting seed. Cannot seed Products.");
                return; // Stop if suppliers are still missing
            }

            logger.LogInformation("Proceeding to seed Products table from SQL file...");

            var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "SQLQuery-Product.sql");

            if (!File.Exists(sqlFilePath))
            {
                logger.LogError($"Product seed script not found at: {sqlFilePath}");
                return;
            }

            try
            {
                string sqlScript = await File.ReadAllTextAsync(sqlFilePath);

                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    await context.Database.ExecuteSqlRawAsync(sqlScript);
                    await transaction.CommitAsync();
                }

                logger.LogInformation("Products table seeded successfully from SQL file.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the Products table from SQL file.");
                throw;
            }
        }
    }
}