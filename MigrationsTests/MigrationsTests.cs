using System.Runtime.CompilerServices;
using Assent;
using MainProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsTests;

public class MigrationsTests
{
    [Fact]
    public async Task AssentAllMigrations()
    {
        static IEnumerable<(string name, string script)> GetMigrationScripts(DbContext dbContext)
        {
            var migrations = dbContext.Database.GetMigrations().ToList();
            var migrator = dbContext.GetService<IMigrator>();

            string? fromMigration = null;
            foreach (var migration in migrations)
            {
                var script = migrator.GenerateScript(fromMigration, migration, MigrationsSqlGenerationOptions.NoTransactions);
                yield return (migration, script);
                fromMigration = migration;
            }
        }

        await using var db = new MyDbContext();

        foreach (var (name, script) in GetMigrationScripts(db))
        {
            void Assent([CallerFilePath] string callerFilePath = null!)
            {
                var path = Path.GetFullPath(Path.Combine(callerFilePath, "..", "..", "MigrationsProject", "Scripts", name));
                this.Assent(script, new Configuration().UsingFixedName(path).UsingExtension("sql").UsingApprovalFileNameSuffix(null));
            }

            Assent();
        }
    }
}