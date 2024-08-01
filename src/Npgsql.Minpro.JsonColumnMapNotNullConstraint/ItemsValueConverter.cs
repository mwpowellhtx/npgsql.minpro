using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Npgsql.Minpro;

public class ItemsValueConverter : ValueConverter<ICollection<string>, string>
{
    public ItemsValueConverter()

#pragma warning disable IDE0002 // Simplify member access, intentional for Expression readability
        : base(items => ItemsValueConverter.ConvertTo(items)
            , jsonString => ItemsValueConverter.ConvertFrom(jsonString))
    {
    }

    public static string ConvertTo(ICollection<string> items)
    {
        Debug.WriteLineIf(items is null, $"{nameof(items)} is null");
        Debug.WriteLineIf(items is not null, $"{nameof(items)} is not null");
        // TODO: closer to what we might want to do in our respective JSON column scenarios
        var converted = JsonConvert.SerializeObject(items);
        return converted;
    }

    public static ICollection<string> ConvertFrom(string jsonString)
    {
        // TODO: ditto except conversion the other way...
        var converted = JsonConvert.DeserializeObject<string[]>(jsonString);
        return converted;
    }
}