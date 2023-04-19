using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Attendant.Contracts.Services;

public interface IBaseService
{
    Task<User> GetUserByToken(string token, CancellationToken cancellationToken);
}