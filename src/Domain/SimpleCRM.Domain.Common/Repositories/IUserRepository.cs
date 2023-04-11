using SimpleCRM.Domain.Common.DTOs;
using SimpleCRM.Domain.Common.Models;

namespace SimpleCRM.Domain.Common.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserAsync(LoginRQ loginRQ, CancellationToken cancellationToken);
}