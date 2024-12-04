using Microsoft.EntityFrameworkCore;
using temuruang_be.Models;

namespace temuruang_be.Services;

public interface IWorkspaceService
{
    Task<Workspace> AddWorkspace(Workspace Workspace) ;
    Task UpdateWorkspace(int id, Workspace Workspace) ;
    Task DeleteWorkspace(Workspace Workspace);
    Task<Workspace?> FetchWorkspaceByID(int id);
    Task<IEnumerable<Workspace>> FetchWorkspaces();
}

public sealed class WorkspaceService : IWorkspaceService 
{
    private readonly ApplicationDbContext dbCtx;

    public WorkspaceService(ApplicationDbContext dbCtx) 
    {
        this.dbCtx = dbCtx;
    }

    public async Task<Workspace> AddWorkspace(Workspace Workspace) 
    {
        dbCtx.Add(Workspace);

        await dbCtx.SaveChangesAsync();

        return Workspace;
    }

    public async Task UpdateWorkspace(int id, Workspace Workspace) 
    {
        Workspace.Id = id;
        dbCtx.Update(Workspace);

        await dbCtx.SaveChangesAsync();
    }

    public async Task DeleteWorkspace(Workspace Workspace)
    {
        dbCtx.Remove(Workspace);

        await dbCtx.SaveChangesAsync();
    }

    public async Task<Workspace?> FetchWorkspaceByID(int id)
    {
        Workspace? Workspace = await dbCtx.Workspace.Where(a => a.Id ==id).AsNoTracking().FirstOrDefaultAsync();

        if (Workspace == null) 
        {
            return null;
        }

        return Workspace;        
    }

    public async Task<IEnumerable<Workspace>> FetchWorkspaces()
    {
        IEnumerable<Workspace> workspaces = await dbCtx.Workspace.AsNoTracking().ToListAsync();

        return workspaces;
    }
}