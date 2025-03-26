using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SignUpController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpForm model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        if (await _userManager.Users.AnyAsync(x => x.Email == model.Email))
            return Conflict(new { error = "User already exists." });

        var user = new User
        {
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };

        var identityResult = await _userManager.CreateAsync(user, model.Password);
        if (identityResult.Succeeded)
        {
            try
            {
                var userRole = model.Role ?? "User";

                if (!await _roleManager.RoleExistsAsync(model.Role!))
                    userRole = "User";

                var result = await _userManager.AddToRoleAsync(user, userRole);
                if (result.Succeeded)
                    return Ok();

                throw new Exception("User was created, but was not added to default role.");
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        return Problem("Unable to create user.");
    }
}
