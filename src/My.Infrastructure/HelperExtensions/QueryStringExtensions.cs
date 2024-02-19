using System.Reflection;
using System.Text.Json.Serialization;
using System.Web;

public static class QueryStringExtensions
{
    public static string ToQueryString(this object queryParams)
    {
        var properties = queryParams.GetType().GetProperties()
            .Where(p => p.GetValue(queryParams, null) != null)
            .Select(p => {
                // Check for JsonPropertyName attribute
                var attribute = p.GetCustomAttribute<JsonPropertyNameAttribute>();
                string name = attribute?.Name ?? p.Name; // Use attribute name if available, otherwise use property name
                string value = HttpUtility.UrlEncode(p.GetValue(queryParams).ToString());
                return $"{name}={value}";
            });

        return String.Join("&", properties);
    }
}
