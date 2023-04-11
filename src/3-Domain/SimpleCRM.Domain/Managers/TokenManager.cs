using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Managers;

public class TokenManager
{
    private readonly string _secret;
    private readonly int _expiresIn;

    public int ExpiresIn => _expiresIn;
    
    public TokenManager(IConfiguration configuration)
    {
        var authentication = configuration.GetSection("Authentication");
        _secret = authentication.GetValue<string>("Secret") ?? string.Empty;
        _expiresIn = authentication.GetValue<int>("ExpiresIn");

        if (string.IsNullOrEmpty(_secret))
            throw new ArgumentException("Authentication.Secret not configured");
        
        if (_expiresIn <= 0)
            throw new ArgumentException("Authentication.ExpiresIn not configured or 0");
    }
    
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(user.Role)!)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_expiresIn),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}