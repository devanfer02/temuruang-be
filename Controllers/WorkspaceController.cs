using Microsoft.AspNetCore.Mvc;
using temuruang_be.Dtos.WorkspaceDTO;
using temuruang_be.Models;
using temuruang_be.Services;

namespace temuruang_be.Controllers;

[ApiController]
[Route("api/workspaces")]
public class WorkspaceController : ControllerBase
{
    private readonly IWorkspaceService _workSvc;
    private readonly ILogger<WorkspaceController> _logger;

    public WorkspaceController(IWorkspaceService workspaceService, ILogger<WorkspaceController> logger) 
    {
        _workSvc = workspaceService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> FetchAllWorkspaces()
    {
        try 
        {
            var workspaces = await _workSvc.FetchWorkspaces();

            return Ok(ApiResponse<IEnumerable<Workspace>>.Create(200, "successfully fetch workspaces", workspaces));
        }
        catch (Exception e) 
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);            
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FetchWorkspaceByID(int id)
    {
        try 
        {
            var workspace = await _workSvc.FetchWorkspaceByID(id);

            if (workspace == null)
            {
                return NotFound(ApiResponse<string>.Create(404, "failed to fetch workspace", "cant find the requested item"));
            }

            return Ok(ApiResponse<Workspace>.Create(200, "successfully fetch workspace", workspace));
        }
        catch (Exception e) 
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);            
        }   
    }

    [HttpPost]
    public async Task<IActionResult> AddWorkspace(WorkspaceRequestDTO dto)
    {
        try 
        {
            if (dto.Name == null)
            {
                return Ok();
            }
            var workspace = await _workSvc.AddWorkspace(WorkspaceRequestDTO.ToWorkspace(dto));

            return Ok(ApiResponse<Workspace>.Create(200, "successfully create workspace", workspace));
        }
        catch (Exception e) 
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);            
        }   
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkspace(int id, WorkspaceRequestDTO dto)
    {
        try 
        {
            var current = await _workSvc.FetchWorkspaceByID(id);

            if (current == null)
            {
                return NotFound(ApiResponse<string?>.Create(404, "failed to update workspace", "cant find the requested item"));
            }

            await _workSvc.UpdateWorkspace(id, WorkspaceRequestDTO.ToWorkspace(dto));

            return Ok(ApiResponse<string?>.Create(200, "successfully update workspace", null));
        }
        catch (Exception e) 
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);            
        }   
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkspace(int id)
    {
        try 
        {
            var current = await _workSvc.FetchWorkspaceByID(id);

            if (current == null)
            {
                return NotFound(ApiResponse<string?>.Create(404, "failed to delete workspace", "cant find the requested item"));
            }

            await _workSvc.DeleteWorkspace(current);

            return Ok(ApiResponse<string?>.Create(200, "successfully delete workspace", null));
        }
        catch (Exception e) 
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);            
        }   
    }
}