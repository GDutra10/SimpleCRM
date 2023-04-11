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
    
    public async Task<User> CreateUserAsync(string name, string email, string password)
    {
        var users = await _userRepository.GetAllAsync(new UserWithEmailSpecification(email));  
        
        if (users.Any())
            throw new BusinessException(nameof(email), "The email already has been used!");

        return new User(name, email, password);
    }
}