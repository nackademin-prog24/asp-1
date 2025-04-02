using Business.Dtos;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Services;

public interface IUserService
{
    Task<bool> CreateUserAsync(AddUserFormData userFormData);
    Task<bool> DeleteUserAsync(string userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string userId);
    Task<IEnumerable<User>?> GetUsersAsync();
    Task<bool> UpdateUserAsync(UpdateUserFormData userFormData);
}

public class UserService(UserRepository userRepository, UserManager<UserEntity> userManager, IMemoryCache cache) : IUserService
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly IMemoryCache _cache = cache;
    private const string _cacheKey_All = "User_All";

    public async Task<bool> CreateUserAsync(AddUserFormData userFormData)
    {
        if (userFormData == null)
            return false;

        var exists = await _userRepository.ExistsAsync(x => x.Email == userFormData.Email);
        if (exists)
            return false;

        var entity = new UserEntity
        {
            UserName = userFormData.Email,
            FirstName = userFormData.FirstName,
            LastName = userFormData.LastName,
            Email = userFormData.Email,
            Address = new UserAddressEntity()
        };

        var result = await _userManager.CreateAsync(entity, userFormData.Password);
        if (result.Succeeded)
        {
            _cache.Remove(_cacheKey_All);
        }

        return result.Succeeded;
    }


    public async Task<IEnumerable<User>?> GetUsersAsync()
    {
        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
            return cachedItems;

        var users = await SetCache();
        return users;
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var user = new User();

        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
        {
            user = cachedItems?.FirstOrDefault(x => x.Id == userId);
            if (user != null)
                return user;
        }

        var entity = await _userRepository.GetAsync(x => x.Id == userId);
        if (entity == null)
            return null;

        await SetCache();

        user = new User
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            PhoneNumber = entity.PhoneNumber,
            StreetName = entity.Address?.StreetName,
            PostalCode = entity.Address?.PostalCode,
            City = entity.Address?.City,
        };
        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = new User();

        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
        {
            user = cachedItems?.FirstOrDefault(x => x.Email == email);
            if (user != null)
                return user;
        }

        var entity = await _userRepository.GetAsync(x => x.Email == email);
        if (entity == null)
            return null;

        await SetCache();

        user = new User
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            PhoneNumber = entity.PhoneNumber,
            StreetName = entity.Address?.StreetName,
            PostalCode = entity.Address?.PostalCode,
            City = entity.Address?.City,
        };
        return user;
    }


    public async Task<bool> UpdateUserAsync(UpdateUserFormData userFormData)
    {
        if (userFormData == null)
            return false;

        var user = await _userRepository.GetAsync(x => x.Id  == userFormData.UserId);
        if (user == null) 
            return false;

        if (user.FirstName != userFormData.FirstName)
            user.FirstName = userFormData.FirstName;

        if (user.LastName != userFormData.LastName)
            user.LastName = userFormData.LastName;

        if (user.Email != userFormData.Email)
            user.Email = userFormData.Email;

        if (user.PhoneNumber != userFormData.PhoneNumber)
            user.PhoneNumber = userFormData.PhoneNumber;

        if (user.Address?.StreetName != userFormData.StreetName)
            user.Address!.StreetName = userFormData.StreetName;

        if (user.Address?.PostalCode != userFormData.PostalCode)
            user.Address!.PostalCode = userFormData.PostalCode;

        if (user.Address?.City != userFormData.City)
            user.Address!.City = userFormData.City;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            _cache.Remove(_cacheKey_All);
        }

        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return false;

        var user = await _userRepository.GetAsync(x => x.Id == userId);
        if (user == null)
            return false;

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            _cache.Remove(_cacheKey_All);
        }

        return result.Succeeded;
    }

    public async Task<IEnumerable<User>> SetCache()
    {
        _cache.Remove(_cacheKey_All);
        var entities = await _userRepository.GetAllAsync();
        var users = entities.Select(x => new User
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email!,
            PhoneNumber = x.PhoneNumber,
            StreetName = x.Address?.StreetName,
            PostalCode = x.Address?.PostalCode,
            City = x.Address?.City,
        });
        users = users.OrderBy(x => x.FirstName);
        _cache.Set(_cacheKey_All, users, TimeSpan.FromMinutes(10));

        return users;
    }
}
