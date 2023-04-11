using Microsoft.Extensions.Logging;
using SimpleCRM.Domain.Common.DTOs;
using SimpleCRM.Domain.Common.Models;
using SimpleCRM.Domain.Common.Providers;
using SimpleCRM.Domain.Common.Repositories;
using SimpleCRM.Domain.Common.Services;

namespace SimpleCRM.Domain.Service.Services;

public class UserService : IUserService
{
    private readonly ILogger<IUserService> _logger;
    private readonly IUserRepository _userRepository;

    public UserService(ILogger<IUserService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    
    public async Task<LoginRS> LoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(loginRQ, cancellationToken);

        // TODO: add converter
        return new LoginRS() { };
    }

    public async Task<InsertUserRS> InsertUserAsync(InsertUserRQ insertUserRQ, CancellationToken cancellationToken)
    {
        // TODO: do it with AutoMapper
        var user = new User
        {
            Email = insertUserRQ.Email,
            Name = insertUserRQ.Name,
            Password = insertUserRQ.Password
        };

        await _userRepository.SaveAsync(user, cancellationToken);

        return new InsertUserRS();
    }
}