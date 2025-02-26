using Microsoft.EntityFrameworkCore;
using UserProfileApi.Models;
using Newtonsoft.Json;

namespace UserProfileApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<UserProfile> UserProfiles { get; set; } = null!;
    public DbSet<ProfileField> ProfileFields { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserProfile>()
            .Property(u => u.CustomFields)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
            );
    }
}