using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController(UserManager<UserEntity> userManager) : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager = userManager;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(string id)
    {
        var users = await _userManager.FindByIdAsync(id);
        return Ok(users);
    }
}
