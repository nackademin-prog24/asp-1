using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IClientService clientService) : ControllerBase
{
    private readonly IClientService _clientService = clientService;

    [HttpPost]
    public async Task<ActionResult<Client>> Create([FromForm] AddClientFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var result = await _clientService.CreateClientAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetAll()
    {
        var result = await _clientService.GetClientsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> Get(int id)
    {
        var result = await _clientService.GetClientByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<Client>> Update(UpdateClientFormData formData)
    {
        var result = await _clientService.UpdateClientAsync(formData);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _clientService.DeleteClientAsync(id);
        return result ? Ok() : NotFound();
    }
}