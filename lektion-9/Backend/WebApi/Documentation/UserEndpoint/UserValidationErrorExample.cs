using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation.UserEndpoint;

public class UserValidationErrorExample : IExamplesProvider<ErrorMessage>
{
    public ErrorMessage GetExamples() => new()
    {
        Message = "Validation failed: Email is required."
    };
}
