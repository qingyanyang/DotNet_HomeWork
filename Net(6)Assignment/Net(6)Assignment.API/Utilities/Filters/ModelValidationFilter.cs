using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Net_6_Assignment.Common;
using System.Linq;

namespace Net_6_Assignment.Utilities.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        private readonly ILogger<ModelValidationFilter> _logger;

        public ModelValidationFilter(ILogger<ModelValidationFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action name: {ActionName}", context.ActionDescriptor.DisplayName);
            _logger.LogInformation("Parameters: {Parameters}", string.Join(", ", context.ActionArguments.Select(arg => $"{arg.Key}: {arg.Value}")));

            // Manually checking model validation
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new CommonResponse<object>
                {
                    IsSuccess = false,
                    Errors = errors
                };

                // Log validation errors
                _logger.LogWarning("Model validation failed: {Errors}", errors);

                // Return bad request with validation errors
                context.Result = new BadRequestObjectResult(response);
                return;
            }
        }
    }
}