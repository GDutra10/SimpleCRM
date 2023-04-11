using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts;

public interface IUserService
{
     // Task<LoginRS> LoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken);
     Task<UserRS> InsertUserAsync(InsertUserRQ insertUserRQ, CancellationToken cancellationToken);
}