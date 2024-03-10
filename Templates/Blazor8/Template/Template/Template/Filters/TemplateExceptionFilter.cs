using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Inteleduc.Web.API.Filters
{
    public class TemplateExceptionFilter() : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;

            Log.Error(context.Exception, errorMessage);

            var errorResult = new ObjectResult(new { Error = errorMessage })
            {
                StatusCode = 400,
                ContentTypes = { "application/json" }
            };

            // Set the result to be the error response
            context.Result = errorResult;
        }
    }
}
