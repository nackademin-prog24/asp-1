using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions.Attributes;

namespace WebApi.Controllers;


[UseAdminApiKey]
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Create(AddUserFormData userFormData)
    {
        if (!ModelState.IsValid)
            return BadRequest(userFormData);

        var result = await _userService.CreateUserAsync(userFormData);
        return result ? Ok() : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        var result = await _userService.GetUserByIdAsync(userId);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserFormData userFormData)
    {
        var result = await _userService.UpdateUserAsync(userFormData);
        return result ? Ok() : NotFound();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _userService.DeleteUserAsync(id);
        return result ? Ok() : NotFound();
    }
}
