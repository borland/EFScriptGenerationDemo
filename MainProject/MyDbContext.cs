using Microsoft.EntityFrameworkCore;

namespace MainProject;

public sealed class MyDbContext : DbContext
{
    static readonly string DbFile = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");

    public DbSet<Thing> Things { get; private set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbFile}");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

public class Thing
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}