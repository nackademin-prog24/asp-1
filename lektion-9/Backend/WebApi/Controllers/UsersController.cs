using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Documentation.UserEndpoint;
using WebApi.Extensions.Attributes;

namespace WebApi.Controllers;

[Authorize]
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    [UseAdminApiKey]
    [Consumes("multipart/form-data")]
    [SwaggerOperation(Summary = "Create a new User", Description = "Only 'admins' can create users. This will require a API-key 'X-ADM-API-KEY' in the header request.")]
    [SwaggerRequestExample(typeof(AddUserForm), typeof(AddUserDataExample))]
    [SwaggerResponseExample(400, typeof(UserValidationErrorExample))]
    [SwaggerResponseExample(409, typeof(UserAlreadyExistsErrorExample))]
    [SwaggerResponse(200, "User successfully created")]
    [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
    [SwaggerResponse(409, "User already exists", typeof(ErrorMessage))]
    public async Task<IActionResult> Create([FromForm] AddUserForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var result = await _userService.CreateUserAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all users", Description = "Only 'admins' can create users. This will require a API-key 'X-ADM-API-KEY' in the header request.")]
    [SwaggerResponse(200, "Returns all users", typeof(IEnumerable<User>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(200, "Returns a user by ID", typeof(User))]
    [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    [UseAdminApiKey]
    [Consumes("multipart/form-data")]
    [SwaggerRequestExample(typeof(UpdateUserForm), typeof(UpdateUserDataExample))]
    [SwaggerResponseExample(200, typeof(UserExample))]
    [SwaggerResponseExample(404, typeof(UserNotFoundExample))]
    [SwaggerResponse(200, "User successfully updated", typeof(User))]
    [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
    public async Task<IActionResult> Update([FromForm] UpdateUserForm formData)
    {
        var result = await _userService.UpdateUserAsync(formData);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    [UseAdminApiKey]
    [SwaggerResponse(200, "User successfully deleted")]
    [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _userService.DeleteUserAsync(id);
        return result ? Ok() : NotFound();
    }
}