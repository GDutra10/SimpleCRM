using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Common.Contracts.Services;

public interface IBaseService
{
    Task<User> GetUserByTokenAsync(string token, CancellationToken cancellationToken);
}