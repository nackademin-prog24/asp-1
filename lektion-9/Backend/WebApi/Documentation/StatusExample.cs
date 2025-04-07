using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation;

public class StatusExample : IExamplesProvider<Status>
{
    public Status GetExamples()
    {
        return new Status
        {
            Id = 1,
            StatusName = "STARTED"
        };
    }
}
