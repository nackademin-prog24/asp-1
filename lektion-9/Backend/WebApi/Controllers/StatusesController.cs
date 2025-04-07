using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusesController(IStatusService statusService) : ControllerBase
{
    private readonly IStatusService _statusService = statusService;

    [HttpPost]
    public async Task<IActionResult> Create(AddStatusFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var result = await _statusService.CreateStatusAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _statusService.GetStatusesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _statusService.GetStatusByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStatusFormData formData)
    {
        var result = await _statusService.UpdateStatusAsync(formData);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _statusService.DeleteStatusAsync(id);
        return result ? Ok() : NotFound();
    }
}
