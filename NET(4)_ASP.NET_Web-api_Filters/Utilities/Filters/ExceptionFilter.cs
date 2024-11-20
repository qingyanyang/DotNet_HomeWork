using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET3Assignment.Common;
using NET3Assignment.Common.Exceptions;

namespace NET3Assignment.Utilities.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var response = new CommonResponse<object>
            {
                IsSuccess = false
            };

            if (exception is A4ValidationException)
            {
                response.Errors = [exception.Message ];
                context.Result = new BadRequestObjectResult(response);
            }
            else if (exception is A4NotFoundException)
            {
                response.Errors = [exception.Message];
                context.Result = new NotFoundObjectResult(response);
            }
            else
            {
                response.Errors = [ "unknow errors occured" ];
                context.Result = new JsonResult(response)
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
    }
}