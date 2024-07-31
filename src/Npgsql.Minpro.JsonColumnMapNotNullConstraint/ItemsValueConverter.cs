using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Npgsql.Minpro;

public class ItemsValueConverter : ValueConverter<ICollection<string>, string>
{
    public ItemsValueConverter()

#pragma warning disable IDE0002 // Simplify member access, intentional for Expression readability
        : base(items => ItemsValueConverter.ConvertTo(items)
            , jsonString => ItemsValueConverter.ConvertFrom(jsonString))
    {
    }

    public static string ConvertTo(ICollection<string> items) => "[]";

    public static ICollection<string> ConvertFrom(string jsonString) => [];
}