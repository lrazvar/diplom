using DataBaseService.Db.Entity;

namespace DataBaseService.Db;
using Microsoft.EntityFrameworkCore;

public sealed class GameDbContext: DbContext
{
    public DbSet<UserData> UserData { get; set; }
    public GameDbContext() { }

    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserData>()
            .HasIndex(u => u.Login)
            .IsUnique();
    }
    
}