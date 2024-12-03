using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using temuruang_be.Models;
using temuruang_be.Dtos.AuthDTO;
using System.Security.Cryptography;

namespace temuruang_be.Services;

public interface IAuthService 
{
    Boolean PasswordMatch(User user, LoginDTO dto);
    string CreateToken(User user);

}

public class AuthService : IAuthService
{
    private readonly IConfiguration _iconf;
    private readonly ApplicationDbContext dbCtx;
    public AuthService(IConfiguration conf, ApplicationDbContext dbCtx)
    {
        _iconf = conf;
        this.dbCtx = dbCtx;

    }

    public bool PasswordMatch(User user, LoginDTO dto)
    {
        var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(dto.Password);
        var hash = sha256.ComputeHash(bytes);
        var hashString = Convert.ToBase64String(hash);
        
        return hashString == user.Password;
    }

    public string CreateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtSettings = _iconf.GetSection("jwt");

        var privateKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim("email", user.Email));

        return ci;
    }
}