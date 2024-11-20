using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net_6_Assignment.Common;
using Net_6_Assignment.Common.Exceptions;

namespace Net_6_Assignment.Utilities.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var response = new CommonResponse<object>
            {
                IsSuccess = false,
                Errors = new List<string>()
            };

            if (exception is A6ValidationException || exception is A6ArgumentException)
            {
                response.Errors.Add(exception.Message);
                context.Result = new BadRequestObjectResult(response);
                _logger.LogWarning("Validation exception: {Message}", exception.Message);
            }
            else if (exception is A6NotFoundException)
            {
                response.Errors.Add(exception.Message);
                context.Result = new NotFoundObjectResult(response);
                _logger.LogWarning("Not found exception: {Message}", exception.Message);
            }
            else
            {
                response.Errors.Add("An unknown error occurred");
                context.Result = new JsonResult(response)
                {
                    StatusCode = 500
                };
                _logger.LogError("Unexpected error: {Message}", exception.Message);
            }

            context.ExceptionHandled = true;
        }
    }
}