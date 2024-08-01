using System;
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

        // Definitely prefer property, preferring field is just a dumb idea.
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);

        modelBuilder.Entity<ModelTemplate>(o =>
        {
            o.ToTable("efcore_minpro_modeltemplate");

            o.HasKey(p => p.Id);

            o.Property(p => p.AddedAt)
                .IsRequired()
                .HasDefaultValue(ModelTemplate.AddedAtDefault);

            o.Property(p => p.Items)
                .HasColumnName("itemsjson")
                .HasColumnType("JSON")
                .IsRequired()
                .HasDefaultValue(ModelTemplate.ItemsDefault)
                .HasConversion(new ItemsValueConverter());
        });

        modelBuilder.UseLowerCaseColumnNamingConvention();
    }
}