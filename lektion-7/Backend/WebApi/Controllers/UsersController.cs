using Data.Entities;
using Data.Repositories;
using Domain.Dtos;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;

    [HttpPost]
    public async Task<IActionResult> Create(UserRegistrationFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var existsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (existsResult.Succeeded)
            return Conflict();

        var userEntity = new UserEntity
        {
            UserName = formData.Email,
            Email = formData.Email,
            Profile = new UserProfileEntity
            {
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                PhoneNumber = formData.PhoneNumber,
            },
            Address = new UserAddressEntity
            {
                StreetName = formData.StreetName,
                StreetNumber = formData.StreetNumber,
                PostalCodeId = "136 57"
            }
        };

        var result = await _userRepository.AddAsync(userEntity, formData.Password);
        if (result.Succeeded)
            return Ok();

        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userRepository.GetAllAsync();
        return Ok(result);
    }
}
