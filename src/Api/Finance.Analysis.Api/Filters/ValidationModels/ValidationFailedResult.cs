using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Finance.Analysis.Api.Filters.ValidationModels;

public class ValidationFailedResult : ObjectResult
{
    public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationErrorResponse(modelState))
    {
        StatusCode = StatusCodes.Status400BadRequest;
    }
}