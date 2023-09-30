using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BubberDinner.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        //* ObjectResult use in Content negotiation
        //* Format the Response
        //* Return object directly this object will be automatically serialized and written to the Response body
        context.Result = new ObjectResult(new { error = "An error occured while processing your request" })
        {
            StatusCode = 500,
        };

        context.ExceptionHandled = true;
    }
}