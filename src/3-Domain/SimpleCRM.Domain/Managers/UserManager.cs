using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class UserManager
{
    private readonly IRepository<User> _userRepository;

    public UserManager(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> CreateUserAsync(User userRequest, string name, string email, string password, Role role, CancellationToken cancellationToken)
    {
        if (userRequest.Role != Role.Admin)
            throw new BusinessException(string.Empty, $"The user '{userRequest.Name}' can't create new Users!");
        
        var users = await _userRepository.GetAllAsync(new UserWithEmailSpecification(email), cancellationToken);  
        
        if (users.Any())
            throw new BusinessException(nameof(email), "The email already has been used!");

        return new User(name, email, password, role);
    }
    
    public async Task<User> TryLoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(new LoginSpecification(email, password), cancellationToken);
        
        if (users.Count != 1)
            throw new BusinessException("", "Email and/or password invalid!");

        return users.First();
    }

    public async Task<User> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(id, cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found");

        return user;
    }
}