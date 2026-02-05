namespace InventoryManagement.Extensions;

public static class HtmxRequestExtensions
{
    public static bool IsHtmx(this HttpRequest request)
    {
        return request.Headers.TryGetValue("HX-Request", out var value) && value == "true";
    }
}
