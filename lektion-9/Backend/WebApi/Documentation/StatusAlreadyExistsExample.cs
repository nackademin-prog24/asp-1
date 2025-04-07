using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation;

public class StatusAlreadyExistsExample : IExamplesProvider<ErrorMessage>
{
    public ErrorMessage GetExamples()
    {
        return new ErrorMessage
        {
            Message = "Status already exists"
        };
    }
}
