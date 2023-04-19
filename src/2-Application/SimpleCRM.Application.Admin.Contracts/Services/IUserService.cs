using SimpleCRM.Application.Admin.Contracts.DTOs;

namespace SimpleCRM.Application.Admin.Contracts.Services;

public interface IUserService
{
     Task<UserRS> UserRegisterAsync(string accessToken, UserRegisterRQ userRegisterRQ, CancellationToken cancellationToken);
}