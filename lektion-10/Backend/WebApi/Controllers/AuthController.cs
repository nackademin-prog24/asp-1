using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService _authService = authService;

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

        var result = await _authService.SignInAsync(model.Email, model.Password);
        if (string.IsNullOrEmpty(result))
            return Unauthorized();

        return Ok();
    }

}
