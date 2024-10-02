using Finance.Analysis.Api.Filters.ValidationModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Finance.Analysis.Api.Filters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid) context.Result = new ValidationFailedResult(context.ModelState);
    }
}