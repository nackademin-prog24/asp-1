using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation.UserEndpoint;

public class UserExample : IExamplesProvider<User>
{
    public User GetExamples() => new()
    {
        Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
        ImageFileName = "u_ab3214b0-14b5-4f23-a8db-6466f465ce6d.png",
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@domain.com",
        PhoneNumber = "+46 73-123 45 67",
        StreetName = "Nordkapsvägen 1",
        PostalCode = "136 57",
        City = "VEGA",
    };
}
