using Business.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation.UserEndpoint;

public class SignInDataExample : IExamplesProvider<SignInForm>
{
    public SignInForm GetExamples() => new()
    {
        Email = "john.doe@domain.com",
        Password = "BytMig123!",
        RememberMe = false,
    };
}