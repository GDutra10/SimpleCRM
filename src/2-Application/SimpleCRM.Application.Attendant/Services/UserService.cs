﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Attendant.Services;

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

    public async Task<UserRS> UserRegisterAsync(UserRegisterRQ userRegisterRQ, CancellationToken cancellationToken)
    {
        var user = await _userManager
            .CreateUserAsync(userRegisterRQ.Name, userRegisterRQ.Email, userRegisterRQ.Password, userRegisterRQ.Role, cancellationToken);
        await _userRepository.SaveAsync(user, cancellationToken);
        
        return _mapper.Map<User, UserRS>(user);
    }
}