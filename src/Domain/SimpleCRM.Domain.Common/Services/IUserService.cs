using SimpleCRM.Domain.Common.DTOs;

namespace SimpleCRM.Domain.Common.Services;

public interface IUserService
{
     Task<LoginRS> LoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken);
     Task<InsertUserRS> InsertUserAsync(InsertUserRQ insertUserRQ, CancellationToken cancellationToken);
}