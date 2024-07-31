using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace Npgsql.Minpro;

internal static class ModelBuilderEstensions
{
    public static ModelBuilder UseLowerCaseColumnNamingConvention(this ModelBuilder modelBuilder)
    {
        static void OnCamelCase(IMutableProperty property)
        {
            var columnName = property.GetColumnName();
            var lowerCaseName = columnName.ToLower();
            property.SetColumnName(lowerCaseName);
        }

        modelBuilder.Model.GetEntityTypes().ToList().ForEach(
            entity => entity.GetProperties().ToList().ForEach(OnCamelCase)
        );

        return modelBuilder;
    }
}