using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Attendant.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<IAuthenticationService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly UserManager _userManager;
    private readonly TokenManager _tokenManager;

    public AuthenticationService(
        ILogger<AuthenticationService> logger, 
        IMapper mapper,
        IRepository<User> userRepository,
        UserManager userManager,
        TokenManager tokenManager)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
        _userManager = userManager;
        _tokenManager = tokenManager;
    }
    
    public async Task<LoginRS> TryLoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken)
    {
        var user = await _userManager.TryLoginAsync(loginRQ.Email, loginRQ.Password, cancellationToken);
        var accessToken = _tokenManager.GenerateToken(user);

        return new LoginRS()
        {
            ExpiresIn = _tokenManager.ExpiresIn,
            AccessToken = accessToken
        };
    }
}