using Business.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation.UserEndpoint;

public class AddUserDataExample : IExamplesProvider<AddUserForm>
{
    public AddUserForm GetExamples() => new()
    {
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@domain.com",
    };
}
