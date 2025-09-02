namespace Technical.Interview.Backend.Controllers;

using ErrorOr;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Route("api/[controller]")]
public abstract class InterviewControllerBase : ControllerBase
{
    protected IActionResult Problem(Error Error)
    {
        var StatusCode = Error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: StatusCode, detail: Error.Description);
    }

    protected IActionResult ValidationProblem(List<Error> Errors)
    {
        var ModelStateDictionary = new ModelStateDictionary();

        foreach (var Error in Errors)
        {
            ModelStateDictionary.AddModelError(Error.Code, Error.Description);
        }

        return ValidationProblem(ModelStateDictionary);
    }

    protected IActionResult Problem(List<Error> Errors)
    {
        if (Errors.Count is 0) Problem();
        if (Errors.All(Error => Error.Type == ErrorType.Validation)) return ValidationProblem(Errors);
        return Problem(Errors[0]);
    }
}
