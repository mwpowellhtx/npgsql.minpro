using Microsoft.EntityFrameworkCore;

namespace Npgsql.Minpro;

public class ModelDbContext : DbContext
{
    public ModelDbContext(DbContextOptions<ModelDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ModelTemplate>(o =>
        {
            o.ToTable("efcore_minpro_modeltemplate");
            o.HasKey(p => p.Id);
            o.Property(p => p.Items).IsRequired().HasColumnName("itemsjson").HasColumnType("json").HasConversion(new ItemsValueConverter());
        });

        modelBuilder.UseLowerCaseColumnNamingConvention();
    }
}