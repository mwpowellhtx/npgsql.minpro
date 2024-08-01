using System;
using System.Diagnostics;
using System.Linq;
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
            // TODO: ditto conventions fluent chaining
            dataSourceBuilder.UseJsonNet();
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
        // TODO: see npgsql/efcore.pg https://github.com/npgsql/efcore.pg/issues/3238
        // TODO: see npgsql https://github.com/npgsql/npgsql/issues/5797
        models.Add(model);

        /* TODO: It seems that, for whatever reason, before or immediately after adding the
         * instance to the set, we must evaluate the properties which shall be serialized.
         * When we do not iterate the property.get aspects, then it seems as though EFCORE
         * and/or NPGSQL are taking liberties, reaching down through the persistence layers
         * for default values. As opposed to the ones we very deliberately, intentionally
         * provided from the model perspective.
         */
        //var model_Items = model.Items;

        /* TODO: happens when, for instance, we do not specify a the mapping having a default value
         * TODO: one other thing we are suspicious toward is the disposition toward NRT-ness being a factor
         * PostgresException: 23502: null value in column "itemsjson" of relation "efcore_minpro_modeltemplate" violates not-null constraint
         * DETAIL: Failing row contains(e47480db-e982-4e0e-b7f1-6d3b0bd5a82b, null).
         */
        context.SaveChanges();

        var other = models.Single(x => x.Id == model.Id);

        Debug.Assert(model.Id != Guid.Empty);
        Debug.Assert(model.Id == other.Id);

        Trace.WriteLine($"Model was added: id = {model.Id:d}, at = {(model.AddedAt ?? DateTime.UtcNow):O}");
    }
}