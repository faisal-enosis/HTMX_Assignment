namespace InventoryManagement.Utility;

public static class Helper
{
    public static string GetEnumDisplayName(Enum enumValue)
    {
        var attr = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();

        return attr?.Name ?? enumValue.ToString();
    }
}
