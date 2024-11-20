using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NET3Assignment.Common;

namespace NET3Assignment.Utilities.Filters
{
    public class CommonResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value != null)
            {
                var resultValue = objectResult.Value;
                if (resultValue is not CommonResponse<object>)
                {
                    // confirm T of CommonResponse<T> 
                    var commonResponseType = typeof(CommonResponse<>).MakeGenericType(resultValue.GetType());

                    // create CommonResponse<T> instance
                    var commonResponse = Activator.CreateInstance(commonResponseType);

                    commonResponseType.GetProperty("Data")?.SetValue(commonResponse, resultValue);

                    commonResponseType.GetProperty("Message")?.SetValue(commonResponse, $"Success - Timestamp: {DateTime.UtcNow}");

                    objectResult.Value = commonResponse;
                }
            }
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
