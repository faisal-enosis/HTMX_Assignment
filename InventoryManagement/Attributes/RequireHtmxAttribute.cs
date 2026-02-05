namespace InventoryManagement.Attributes;

public class RequireHtmxAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var isHtmx = context.HttpContext.Request.IsHtmx();
        if (!isHtmx)
        {
            context.Result = new NotFoundResult();
        }

        base.OnActionExecuting(context);
    }
}
