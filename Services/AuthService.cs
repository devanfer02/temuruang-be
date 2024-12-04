using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using temuruang_be.Models;
using temuruang_be.Dtos.AuthDTO;
using System.Security.Cryptography;
using temuruang_be.Utils;

namespace temuruang_be.Services;

public interface IAuthService 
{
    bool PasswordMatch(User user, LoginDTO dto);
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
        return user.Password == Hash.HashPassword(dto.Password);
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

       var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new Claim("fullname", user.Fullname)
        };

        var token = new JwtSecurityToken(
            issuer: _iconf["Jwt:Issuer"],
            audience: _iconf["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_iconf["Jwt:ExpireMinutes"])),
            signingCredentials: credentials
        );
    
        return handler.WriteToken(token);
    }


}