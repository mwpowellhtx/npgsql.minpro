using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Npgsql.Minpro;

internal class Program
{
    private static IServiceCollection Services { get; } = new ServiceCollection();

    private static IServiceProvider Provider { get; set; }

    static void Main(string[] args)
    {
        Services.AddDbContext<ModelDbContext>(contextOptions =>
        {
            var connectionString = Environment.GetEnvironmentVariable("NPGSQL_MINPRO_CONNECTION_STRING");
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString).EnableDynamicJson();
            var dataSource = dataSourceBuilder.Build();
            contextOptions.UseNpgsql(dataSource);
        }, ServiceLifetime.Singleton);

        Provider = Services.BuildServiceProvider();

        // TODO: could arrange database transaction but will not worry about that here for now
        using var context = Provider.GetService<ModelDbContext>();

        var models = context.Set<ModelTemplate>();

        ModelTemplate model = new();

        // TODO: there is nothing about the model, value converter, mappings, database table column creation script, no-thing...
        // TODO: that should be translated, interpreted, transcribed, ad nauseam, ad infinitum, as a 'null'.
        models.Add(model);

        /* PostgresException: 23502: null value in column "itemsjson" of relation "efcore_minpro_modeltemplate" violates not-null constraint
         * DETAIL: Failing row contains(e47480db-e982-4e0e-b7f1-6d3b0bd5a82b, null).
         */
        context.SaveChanges();

    Debug.Assert(model.Id != Guid.Empty);

        Console.WriteLine($"Model was added: {model.Id:d}");
    }
}