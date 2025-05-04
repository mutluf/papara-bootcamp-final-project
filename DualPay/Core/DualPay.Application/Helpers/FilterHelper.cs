namespace DualPay.Application.Helpers;

public static class FilterHelper
{
    public static Dictionary<string, object> ParseFilters(string[] filters)
    {
        var parsedFilters = new Dictionary<string, object>();

        foreach (var filter in filters)
        {
            var parts = filter.Split('=');

            if (parts.Length == 2)
            {
                parsedFilters.Add(parts[0], parts[1]);
            }
        }

        return parsedFilters;
    }
}
