using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Common.Contracts.Services;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Common.Services;

public abstract class BaseService : IBaseService
{
    protected readonly ILogger<IBaseService> Logger;
    protected readonly IMapper Mapper;
    protected readonly TokenManager TokenManager;
    protected readonly UserManager UserManager;

    protected BaseService(
        ILogger<BaseService> logger,
        IMapper mapper,
        TokenManager tokenManager,
        UserManager userManager)
    {
        Logger = logger;
        Mapper = mapper;
        TokenManager = tokenManager;
        UserManager = userManager;
    }
    
    public async Task<User> GetUserByToken(string token, CancellationToken cancellationToken)
    {
        var userId = TokenManager.GetId(token);
        
        return await UserManager.GetUserAsync(userId, cancellationToken);
    } 
}