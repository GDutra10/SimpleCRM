using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Application.Common.Contracts.Services;

public interface IAuthenticationService
{
    Task<LoginRS> TryLoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken);
}