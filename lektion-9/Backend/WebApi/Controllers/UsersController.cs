using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Create(AddUserFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var result = await _userService.CreateUserAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }


    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserFormData formData)
    {
        var result = await _userService.UpdateUserAsync(formData);
        return result != null ? Ok(result) : NotFound();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _userService.DeleteUserAsync(id);
        return result ? Ok() : NotFound();
    }
}
