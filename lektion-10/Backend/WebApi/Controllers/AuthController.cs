using Business.Dtos;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(AuthService authService, SignInManager<UserEntity> signInManager) : ControllerBase
{
    private readonly AuthService _authService = authService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.SignUpAsync(model);
        return result ? Ok() : BadRequest(ModelState);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SignInDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var authResult = await _authService.SignInAsync(model.Email, model.Password);
        if (authResult == null || string.IsNullOrEmpty(authResult.AccessToken))
            return Unauthorized();

        return Ok(authResult);
    }

    [HttpGet("signout")]
    public async Task<IActionResult> Signout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

}
