using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation.UserEndpoint;

public class UserNotFoundExample : IExamplesProvider<ErrorMessage>
{
    public ErrorMessage GetExamples() => new()
    {
        Message = "User with the given ID was not found."
    };
}

public class UserAlreadyExistsErrorExample : IExamplesProvider<ErrorMessage>
{
    public ErrorMessage GetExamples() => new()
    {
        Message = "User with the given 'email address' already exists."
    };
}