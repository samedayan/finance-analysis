namespace Finance.Analysis.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class MultipleDescriptionAttribute(params string[] description) : Attribute
{
    public string[] Description { get; set; } = description;
}