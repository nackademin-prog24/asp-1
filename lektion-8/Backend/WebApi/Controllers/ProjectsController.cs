using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> Create(AddProjectFormData projectFormData)
    {
        if (!ModelState.IsValid)
            return BadRequest(projectFormData);

        var result = await _projectService.CreateProjectAsync(projectFormData);
        return result ? Ok() : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _projectService.GetProjectsAsync();
        return Ok(result);
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> Get(string projectId)
    {
        var result = await _projectService.GetProjectByIdAsync(projectId);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProjectFormData projectFormData)
    {
        var result = await _projectService.UpdateProjectAsync(projectFormData);
        return result ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _projectService.DeleteProjectAsync(id);
        return result ? Ok() : NotFound();
    }
}
