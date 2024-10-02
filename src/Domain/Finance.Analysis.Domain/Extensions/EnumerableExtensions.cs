namespace Finance.Analysis.Domain.Extensions;

public static class EnumerableExtensions
{
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? data)
    {
        return data is not null && data.Any();
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? data)
    {
        return data is null || !data.Any();
    }
}