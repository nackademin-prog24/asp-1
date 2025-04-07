using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Documentation.UserEndpoint;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService, ITokenService tokenService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("SignUp")]
        [SwaggerOperation(Summary = "Sign Up as a new User")]
        [SwaggerRequestExample(typeof(SignUpForm), typeof(SignUpDataExample))]
        [SwaggerResponseExample(400, typeof(UserValidationErrorExample))]
        [SwaggerResponseExample(409, typeof(UserAlreadyExistsErrorExample))]
        [SwaggerResponse(200, "User successfully created")]
        [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponse(409, "User already exists", typeof(ErrorMessage))]
        public async Task<IActionResult> SignUp(SignUpForm formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _userService.CreateUserAsync(formData);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost("SignIn")]
        [SwaggerOperation(Summary = "Sign In with a existing User")]
        [SwaggerRequestExample(typeof(SignInForm), typeof(SignInDataExample))]
        [SwaggerResponseExample(401, typeof(SignInErrorExample))]
        [SwaggerResponse(200, "User successfully created")]
        [SwaggerResponse(401, "Invalid email or password", typeof(ErrorMessage))]
        public async Task<IActionResult> SignIn(SignInForm formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _userService.LoginUserAsync(formData); 
            if (result)
            {
                var user = await _userService.GetUserByEmailAsync(formData.Email);

                var token = _tokenService.GenerateToken(user!.Id, user.Email);
                return Ok(new { token });
            }

            return Unauthorized(new ErrorMessage { Message = "Invalid email or password." });
        }
    }
}
