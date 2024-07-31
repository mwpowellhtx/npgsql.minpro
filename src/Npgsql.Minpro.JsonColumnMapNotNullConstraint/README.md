### Npgsql.Minpro.JsonColumnMapNotNullConstraint

Demonstrates violations of the NOT NULL constraint, even though there is nothing whatsoever about the model, Entity Framework Core mapping, value converter, nothing, that can be translated or otherwise interpreted as a `null`.

The error as reported by exception during `DbContext.SaveChanges()`.

```
PostgresException: 23502: null value in column "itemsjson" of relation "efcore_minpro_modeltemplate" violates not-null constraint
DETAIL: Failing row contains(e47480db-e982-4e0e-b7f1-6d3b0bd5a82b, null).
```

### Caveats

* Add `NPGSQL_MINPRO_CONNECTION_STRING` to your environment variable, which by the name, should be your connection string. Decoupled from the demo for security purposes
