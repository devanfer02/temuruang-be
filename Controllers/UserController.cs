using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using temuruang_be.Dtos.UserDTO;
using temuruang_be.Models;
using temuruang_be.Services;

namespace temuruang_be.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userSvc;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userSvc = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> FetchAllUsers()
    {
        try 
        {
            var users = await _userSvc.FetchUsers();

            return Ok(ApiResponse<IEnumerable<FetchUserDTO>>.Create(200, "successfully fetch all users", users));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);            
        }
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> FetchUserByID()
    {
        try 
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null) 
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            var id = Guid.Parse(userId);

            FetchUserDTO? user = await _userSvc.FetchUserByID(id);

            if (user == null)
            {
                return NotFound(ApiResponse<FetchUserDTO?>.Create(404, "failed to find user", null));
            }

            return Ok(ApiResponse<FetchUserDTO>.Create(200, "successfully fetch user by id", user));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(CreateUserDTO dto)
    {
        try 
        {
            var user = await _userSvc.AddUser(dto);
            return Ok(ApiResponse<FetchUserDTO>.Create(200, "successfully add user", user));
        } 
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UpdateUserDTO dto)
    {
        try 
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null) 
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            var id = Guid.Parse(userId);

            dto.Id = id; 

            var result = await _userSvc.UpdateUser(id, dto);

            if (!result) 
            {
                return NotFound(ApiResponse<FetchUserDTO?>.Create(404, "failed to update user", null));   
            }

            return Ok(ApiResponse<string?>.Create(200, "successfully update user", null));
        } 
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try 
        {
            FetchUserDTO? user = await _userSvc.FetchUserByID(id);

            if (user == null )
            {
                return NotFound(ApiResponse<FetchUserDTO?>.Create(404, "failed to delete user", null));   
            }

            await _userSvc.DeleteUser(FetchUserDTO.ToUser(user));

            return Ok(ApiResponse<string?>.Create(200, "successfully delete user", null));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}