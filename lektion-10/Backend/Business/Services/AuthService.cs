using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services;

public class AuthService(JwtTokenHandler jwtTokenHandler, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;

    public async Task<bool> SignUpAsync(SignUpDto signUpForm)
    {
        var user = AuthMapper.MapToEntity(signUpForm);
        var result = await _userManager.CreateAsync(user, signUpForm.Password);

        if (result.Succeeded)
        {
            if (!await _userManager.Users.AnyAsync())
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, signUpForm.Role);
            }
        }

        return result.Succeeded;
    }

    public async Task<string> SignInAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenHandler.GenerateJwtToken(user.Id, user.Email!, roles);
                return token;
            }
        }

        return null!;

    }
}
