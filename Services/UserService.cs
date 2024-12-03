using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using temuruang_be.Dtos.UserDTO;
using temuruang_be.Models;

namespace temuruang_be.Services;

public interface IUserService
{
    Task<FetchUserDTO> AddUser(CreateUserDTO dto);
    Task UpdateUser(Guid id, UpdateUserDTO dto);
    Task DeleteUser(User user);
    Task<FetchUserDTO?> FetchUserByID(Guid id);
    Task<User?> FetchUserByEmail(string email);
    Task<IEnumerable<FetchUserDTO>> FetchUsers();
}

public sealed class UserService : IUserService 
{
    private readonly ApplicationDbContext dbCtx;

    public UserService(ApplicationDbContext dbCtx) 
    {
        this.dbCtx = dbCtx;
    }

    public async Task<FetchUserDTO> AddUser(CreateUserDTO dto) 
    {
        User user = CreateUserDTO.ToUser(dto);

        var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(user.Password);
        var hash = sha256.ComputeHash(bytes);
        var hashString = Convert.ToBase64String(hash);
        user.Password = hashString;

        dbCtx.Add(user);

        await dbCtx.SaveChangesAsync();

        return User.ToFetchUserDTO(user);
    }

    public async Task UpdateUser(Guid id, UpdateUserDTO dto) 
    {
        User? existingUser = await dbCtx.User.Where(u => u.Id ==id).AsNoTracking().FirstOrDefaultAsync();

        if (existingUser == null) 
        {
            return;
        }

        User user = UpdateUserDTO.ToUser(dto, existingUser);

        dbCtx.Update(user);

        await dbCtx.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        dbCtx.Remove(user);

        await dbCtx.SaveChangesAsync();
    }

    public async Task<FetchUserDTO?> FetchUserByID(Guid id)
    {
        User? user = await dbCtx.User.Where(u => u.Id ==id).AsNoTracking().FirstOrDefaultAsync();

        if (user == null) 
        {
            return null;
        }

        return User.ToFetchUserDTO(user);        
    }

    public async Task<User?> FetchUserByEmail(string email) 
    {
        User? user = await dbCtx.User.Where(u => u.Email == email).AsNoTracking().FirstOrDefaultAsync();

        return user;
    }

    public async Task<IEnumerable<FetchUserDTO>> FetchUsers()
    {
        IEnumerable<User> users = await dbCtx.User.AsNoTracking().ToListAsync();

        return users.Select(User.ToFetchUserDTO);
    }
}