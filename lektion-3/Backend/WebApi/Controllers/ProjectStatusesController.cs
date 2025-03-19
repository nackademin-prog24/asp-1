using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectStatusesController(ProjectStatusService projectStatusService) : ControllerBase
{
    private readonly ProjectStatusService _projectStatusService = projectStatusService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var statuses = await _projectStatusService.GetProjectStatuses();
        return Ok(statuses);
    }
}
