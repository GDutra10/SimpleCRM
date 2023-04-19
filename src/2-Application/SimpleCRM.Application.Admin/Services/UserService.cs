using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Admin.Contracts.DTOs;
using SimpleCRM.Application.Admin.Contracts.Services;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Admin.Services;

public class UserService : IUserService
{
    private readonly ILogger<IUserService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly UserManager _userManager;
    private readonly TokenManager _tokenManager;

    public UserService(
        ILogger<UserService> logger, 
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
    
    public async Task<UserRS> UserRegisterAsync(string accessToken, UserRegisterRQ userRegisterRQ, CancellationToken cancellationToken)
    {
        var id = _tokenManager.GetId(accessToken);
        var userRequest = await _userRepository.GetAsync(id, cancellationToken);

        if (userRequest is null)
            throw new ValidationException("Invalid User!");
        
        var userNew = await _userManager
            .CreateUserAsync(userRequest, userRegisterRQ.Name, userRegisterRQ.Email, userRegisterRQ.Password, userRegisterRQ.Role, cancellationToken);
        await _userRepository.SaveAsync(userNew, cancellationToken);
        
        return _mapper.Map<User, UserRS>(userNew);
    }
}