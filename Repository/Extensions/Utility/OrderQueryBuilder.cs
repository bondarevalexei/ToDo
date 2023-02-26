using System.Reflection;
using System.Text;

namespace Repository.Extensions.Utility;

public class OrderQueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQueryString)
    {
        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propFromQueryName = param.Split(" ")[0];
            var objProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(
                propFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objProperty is null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objProperty.Name.ToString()} {direction},");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        return orderQuery;
    }
}
