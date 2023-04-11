using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Managers;

public class TokenManager
{
    private readonly string _secret;
    private readonly int _expiresIn;
    private readonly ILogger<TokenManager> _logger;

    public int ExpiresIn => _expiresIn;

    public TokenManager(IConfiguration configuration, ILogger<TokenManager> logger)
    {
        var authentication = configuration.GetSection("Authentication");
        _logger = logger;
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

    public Guid GetId(string accessToken)
    {
        var id = string.Empty;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(accessToken);
            id = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when trying to get Id from AccessToken");
        }

        if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var guidOutput))
            throw new BusinessException("Invalid AccessToken");

        return guidOutput;
    }
}