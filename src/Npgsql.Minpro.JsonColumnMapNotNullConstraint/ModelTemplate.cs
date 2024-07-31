using System;
using System.Collections.Generic;

namespace Npgsql.Minpro;

public class ModelTemplate
{
    private Guid _id = Guid.Empty;

    public virtual Guid Id
    {
        get => _id;
        set => _id = value;
    }

    // Does not matter what we populate with, could be empty, but the mapping must succeed.
    private ICollection<string> _items;

    public virtual ICollection<string> Items
    {
        // Key ensuring value:
        get => _items ??= [];
        //            ^^^^^^
        set => _items = value ?? [];
        //            ^^^^^^^^^^^^^
    }

    public ModelTemplate()
    {
    }
}