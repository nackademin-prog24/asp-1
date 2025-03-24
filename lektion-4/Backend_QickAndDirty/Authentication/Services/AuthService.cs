using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services;

public interface IAuthService
{
    Task<SignInResult> SignInAsync(SignInform form);
    Task<IdentityResult> SignUpAsync(SignUpForm form);
}

public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signManager) : IAuthService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signManager = signManager;

    public async Task<IdentityResult> SignUpAsync(SignUpForm form)
    {
        var appUser = new AppUser
        {
            UserName = form.Email,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email
        };

        appUser.Address = new AppUserAddress
        {
            UserId = appUser.Id,
        };

        var result = await _userManager.CreateAsync(appUser, form.Password);
        return result;
    }

    public async Task<SignInResult> SignInAsync(SignInform form)
    {
        var result = await _signManager.PasswordSignInAsync(form.Email, form.Password, false, false);
        return result;
    }


}
