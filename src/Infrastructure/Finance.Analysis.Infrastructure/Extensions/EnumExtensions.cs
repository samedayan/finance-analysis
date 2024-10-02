using System.ComponentModel;
using Finance.Analysis.Infrastructure.Attributes;

namespace Finance.Analysis.Infrastructure.Extensions;

public static class EnumExtensions
{
    public static T ToEnum<T>(this string value, T defaultValue) where T : struct
    {
        if (string.IsNullOrEmpty(value)) return defaultValue;

        T result;
        return Enum.TryParse(value, true, out result) ? result : defaultValue;
    }

    public static string? GetDescription<T>(this T enumValue)
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return default;

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString() ?? string.Empty);

        if (fieldInfo == null) return description;
        var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
        if (attrs.Length > 0) description = ((DescriptionAttribute)attrs[0]).Description;

        return description;
    }

    public static string[]? GetMultipleDescription<T>(this T enumValue)
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return default;

        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString() ?? string.Empty);

        if (fieldInfo == null) return null;
        var attrs = fieldInfo.GetCustomAttributes(typeof(MultipleDescriptionAttribute), true);
        return attrs.Length > 0
            ? ((MultipleDescriptionAttribute)attrs[0])?.Description?.Where(x => !string.IsNullOrWhiteSpace(x))
            ?.Select(x => x.Trim())?.ToArray()
            : null;
    }

    public static T? GetEnumValueFromDescription<T>(string description)
    {
        var type = typeof(T);
        if (!type.IsEnum)
            throw new ArgumentException();
        var fields = type.GetFields();
        var field = fields
            .SelectMany(f => f.GetCustomAttributes(
                typeof(DescriptionAttribute), false), (
                f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                .Description == description);
        return field == null
            ? default
            : (T)field.Field.GetRawConstantValue()!;
    }
}