using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Npgsql.Minpro;

public class ModelTemplate
{
    private Guid _id = Guid.Empty;

    public virtual Guid Id
    {
        get => _id;
        set => _id = value;
    }

    public static DateTime AddedAtDefault => DateTime.UtcNow;

    private DateTime? _addedAt;

    public virtual DateTime? AddedAt
    {
        get => _addedAt;
        set => _addedAt = value;
    }

    public static ICollection<string> ItemsDefault => [];

    // Does not matter what we populate with, could be empty, but the mapping must succeed.
    private ICollection<string> _items;

    public virtual ICollection<string> Items
    {
        // Key ensuring value:
        get
        {
            Trace.WriteLineIf(_items is null, $"{nameof(_items)} value is null");
            Trace.WriteLineIf(_items is not null, $"{nameof(_items)} value is not null: Count = {_items?.Count ?? -1}");
            return _items ??= [$"{DateTime.UtcNow:O}"];
            //            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        }
        set => _items = value ?? ItemsDefault;
        //            ^^^^^^^^^^^^^^^^^^^^^^^
    }

    public ModelTemplate()
    {
    }
}