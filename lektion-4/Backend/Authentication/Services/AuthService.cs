using Authentication.Managers;
using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Authentication.Services;

public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IConfiguration _configuration = configuration;

    #region SignUp

    public async Task<SignUpResult> SignUpAsync(SignUp model)
    {
        if (await _userManager.Users.AnyAsync(x => x.UserName == model.Email))
            return new SignUpResult { Succeeded = false, Error = "User already exists" };

        var result = await RegisterAsync(model.Email, model.Password);
        if (result.Succeeded)
        {
            // create user profile
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser != null)
            {
                var updateProfileResult = await SetUserProfileAsync(appUser, model);

                appUser.Address = new AppUserAddress
                {
                    UserId = appUser.Id,
                    StreetName = model.StreetName,
                    PostalCode = model.PostalCode,
                    City = model.City,
                };

                await _userManager.UpdateAsync(appUser);
                return new SignUpResult { Succeeded = true };
            }

            return new SignUpResult { Succeeded = true, Error ="Succeeded with errors" };

        }

        return new SignUpResult { Succeeded = false, Error = "Unable to create user account." };

    }

    #endregion

    #region Login


    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var result = await AuthenticateAsync(email, password);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = TokenManager.GenerateJwtToken(user, _configuration);
                return new LoginResult { Succeeded = true, Token = token };
            }
        }

        return new LoginResult { Succeeded = false };
    }

    #endregion

    #region Specific Methods 

    public async Task<bool> SetUserProfileAsync(AppUser appUser, SetAppUserProfile profile )
    {
        appUser.Profile = new AppUserProfile
        {
            UserId = appUser.Id,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            PhoneNumber = profile.PhoneNumber
        };
        var result = await _userManager.UpdateAsync(appUser);
        return result.Succeeded;
    }



    public async Task<SignInResult> AuthenticateAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return SignInResult.Failed;

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return IdentityResult.Failed(new IdentityError { Description = "email and password can't be null" });

        var appUser = new AppUser
        {
            UserName = email,
            Email = email,
        };

        var result = await _userManager.CreateAsync(appUser, password);
        return result;
    }

    public async Task<IdentityResult> UnregisterAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return IdentityResult.Failed(new IdentityError { Description = "userId can't be null" });

        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser == null)
            return IdentityResult.Failed(new IdentityError { Description = "User can't be found." });

        var result = await _userManager.DeleteAsync(appUser);
        return result;
    }

    #endregion
}
