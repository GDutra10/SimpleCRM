using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Attendant.Contracts;

public interface IBaseService
{
    Task<User> GetUserByToken(string token, CancellationToken cancellationToken);
}