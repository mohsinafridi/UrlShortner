using Microsoft.EntityFrameworkCore;

namespace UrlShortner.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            dbContext.Database.Migrate();
        }
    }
}
