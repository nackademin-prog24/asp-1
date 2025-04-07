using Business.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation;

public class AddStatusDataExample : IExamplesProvider<AddStatusForm>
{
    public AddStatusForm GetExamples()
    {
        return new AddStatusForm()
        {
            StatusName = "STARTED"
        };
    }
}
