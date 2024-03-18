using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper;

public static class DapperTypeMapServiceCollectionExtensions
{
    public static IServiceCollection AddDapperTypeMapProvider(this IServiceCollection services)
    {
        SqlMapper.TypeMapProvider = (Type type) =>
        {
            // create fallback default type map
            var fallback = new DefaultTypeMap(type);
            return new CustomPropertyTypeMap(type, (t, column) =>
            {
                var property = t.GetProperties().FirstOrDefault(prop =>
                    prop.GetCustomAttributes(false)
                        .OfType<ColumnAttribute>()
                        .Any(attr => attr.Name == column));

                // if no property matched - fall back to default type map
                if (property is null)
                {
                    property = fallback.GetMember(column)?.Property;
                }

                if (property is null)
                {
                    throw new Exception("Cannot find property");
                }

                return property;
            });
        };
        return services;
    }
}
