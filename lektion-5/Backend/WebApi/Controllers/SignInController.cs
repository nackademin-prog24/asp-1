using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Handlers;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SignInController(SignInManager<User> signInManager, UserManager<User> userManager, JwtTokenHandler jwtTokenHandler) : ControllerBase
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInForm model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (signInResult.Succeeded)
                {
                    var token = _jwtTokenHandler.GenerateToken(user!, "User");
                    return Ok(new { token });
                }
            }
        }

        return Unauthorized(new { error = "Invalid email or password." });
    }
}
