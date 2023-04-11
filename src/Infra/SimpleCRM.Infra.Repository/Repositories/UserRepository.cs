using SimpleCRM.Domain.Common.DTOs;
using SimpleCRM.Domain.Common.Models;
using SimpleCRM.Domain.Common.Providers;
using SimpleCRM.Domain.Common.Repositories;

namespace SimpleCRM.Infra.Repository.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(IDbProvider<User> userDbProvider) : base (userDbProvider) { }
    
    public Task<User> GetUserAsync(LoginRQ loginRQ, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}