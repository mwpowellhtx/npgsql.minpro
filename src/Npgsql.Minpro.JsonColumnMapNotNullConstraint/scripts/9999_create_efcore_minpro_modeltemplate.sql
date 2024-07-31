DROP TABLE IF EXISTS efcore_minpro_modeltemplate;

-- modelBuilder.Entity<ModelTemplate>(o =>
-- {
--     o.ToTable("efcore_minpro_modeltemplate");
--     o.HasKey(p => p.Id).HasName("id");
--     o.Property(p => p.Items).IsRequired().HasColumnName("itemsjson").HasConversion(new ItemsValueConverter());
-- });

CREATE TABLE IF NOT EXISTS efcore_minpro_modeltemplate (
  id UUID NOT NULL DEFAULT gen_random_uuid(),
  itemsJson JSON NOT NULL DEFAULT '[]'::JSON,
    CONSTRAINT pk_efcore_minpro_modeltemplate PRIMARY KEY (id)
);
