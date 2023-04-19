using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts.Services;

public interface IAuthenticationService
{
    Task<LoginRS> TryLoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken);
}