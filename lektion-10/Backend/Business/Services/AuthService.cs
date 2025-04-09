using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services;

public class AuthService(JwtTokenHandler jwtTokenHandler, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IConfiguration configuration)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;
    private readonly IConfiguration _configuration = configuration;

    public async Task<bool> SignUpAsync(SignUpDto signUpForm)
    {
        var user = AuthMapper.MapToEntity(signUpForm);
        var userRole = "User";

        if (!await _userManager.Users.AnyAsync())
            userRole = "Admin";

        var result = await _userManager.CreateAsync(user, signUpForm.Password);
        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, userRole);

        return result.Succeeded;
    }

    public async Task<AuthResult> SignInAsync(string email, string password)
    {

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        if (result.Succeeded)
        {
            var response = new AuthResult();

            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenHandler.GenerateJwtToken(user.Id, user.Email!, roles);
                response.AccessToken = token;


                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (isAdmin)
                {
                    response.IsAdmin = true;

                    var adminApiKey = _configuration["ApiKeys:Admin"];
                    response.ApiKey = adminApiKey;
                }

                return response;
            }
        }

        return null!;

    }
}
