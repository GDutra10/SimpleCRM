using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts;

public interface IUserService
{
     Task<UserRS> UserRegisterAsync(UserRegisterRQ userRegisterRQ, CancellationToken cancellationToken);
}