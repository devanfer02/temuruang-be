using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using temuruang_be.Dtos.AuthDTO;
using temuruang_be.Dtos.UserDTO;
using temuruang_be.Models;
using temuruang_be.Services;

namespace temuruang_be.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userSvc;
    private readonly IAuthService _authSvc;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userSvc, IAuthService authSvc, ILogger<AuthController> logger)
    {
        _userSvc = userSvc;
        _authSvc = authSvc;
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDTO credential)
    {
        try 
        {
            var user = await _userSvc.FetchUserByEmail(credential.Email);

            if (user == null)
            {
                return BadRequest(ApiResponse<string?>.Create(400, "invalid email or password", null));
            }   

            if (!_authSvc.PasswordMatch(user, credential)) 
            {
                return BadRequest(ApiResponse<string?>.Create(400, "invalid email or password", null));
            }

            var token = _authSvc.CreateToken(user);

            return Ok(ApiResponse<Dictionary<string, string>>.Create(200, "successfully login user", new Dictionary<string, string>(){
                {"token", token}
            }));
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(CreateUserDTO account)
    {
        try
        {
            var user = await _userSvc.AddUser(account);
            return Ok(ApiResponse<string?>.Create(200, "successfully register user", null));
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}