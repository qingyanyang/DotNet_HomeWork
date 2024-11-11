using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET3Assignment.Common;

namespace NET3Assignment.Utilities.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("action name: "+ context.ActionDescriptor.DisplayName);
            Console.WriteLine("parameters: "+ context.ActionArguments);
            // manually checking model validtion
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                var response = new CommonResponse<object>
                {
                    IsSuccess = false,
                    Errors = errors
                };
                // bad request
                context.Result = new BadRequestObjectResult(response);
                return;
            }
        }
    }
}