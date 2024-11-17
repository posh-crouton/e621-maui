using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E621Maui.Lib.SettingsManager
{
    public partial class SettingsManager
    {
        private string GetJsonPropertyName(string key)
        {
            PropertyInfo[] properties = GetType().GetProperties();

            if (properties.All(x => x.Name != key))
            {
                throw new ArgumentOutOfRangeException($"Property \"{key}\" was not found in \"{nameof(SettingsManager)}\" implementation");
            }

            PropertyInfo property = properties.Single(x => x.Name == key);
            object[] attributes = property.GetCustomAttributes(true);

            return attributes.Count(x => x is JsonPropertyNameAttribute) switch
            {
                0 => key,
                1 => (attributes.Single(x => x is JsonPropertyNameAttribute) as JsonPropertyNameAttribute)!.Name,
                _ => throw new Exception(
                    $"Could not determine JSON property name for \"{key}\" as it has multiple {nameof(JsonPropertyNameAttribute)}s")
            };
        }
    }
}
