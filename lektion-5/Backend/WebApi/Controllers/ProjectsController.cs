using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var items = new List<string>()
        {
            "P1",
            "P2"
        };

        return Ok(items);
    }
}
