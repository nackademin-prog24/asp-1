using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() 
    {
        return Ok();
    }
}
