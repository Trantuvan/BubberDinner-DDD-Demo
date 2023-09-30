using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BubberDinner.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemDetail = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "An error occured while processing your request",
            Status = (int)HttpStatusCode.InternalServerError,
        };

        //* ObjectResult use in Content negotiation
        //* Format the Response
        //* Return object directly this object will be automatically serialized and written to the Response body
        context.Result = new ObjectResult(problemDetail);
        context.ExceptionHandled = true;
    }
}