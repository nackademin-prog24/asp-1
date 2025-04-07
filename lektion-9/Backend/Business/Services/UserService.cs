﻿using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IUserService
{
    Task<User?> CreateUserAsync(AddUserForm formData);
    Task<User?> CreateUserAsync(SignUpForm formData);
    Task<bool> LoginUserAsync(SignInForm formData);
    Task<bool> DeleteUserAsync(string id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string id);
    Task<IEnumerable<User>?> GetUsersAsync();
    Task<User?> UpdateUserAsync(UpdateUserForm formData);
}

public class UserService(UserRepository userRepository, UserManager<UserEntity> userManager, ICacheHandler<IEnumerable<User>> cacheHandler, SignInManager<UserEntity> signInManager) : IUserService
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly ICacheHandler<IEnumerable<User>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Users";


    public async Task<bool> LoginUserAsync(SignInForm formData)
    {
        var exists = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (!exists)
            return false;

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.RememberMe, false);
        return result.Succeeded;
    }

    public async Task<User?> CreateUserAsync(SignUpForm formData)
    {
        var exists = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (exists)
            return null!;

        var entity = UserMapper.ToEntity(formData);
        await _userManager.CreateAsync(entity, formData.Password);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Email == formData.Email);
    }

    public async Task<User?> CreateUserAsync(AddUserForm formData)
    {
        var exists = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (exists)
            return null!;

        var entity = UserMapper.ToEntity(formData);
        await _userManager.CreateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Email == formData.Email);
    }

    public async Task<IEnumerable<User>?> GetUsersAsync()
    {
        var models = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return models;
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Email == email);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Email == email);
    }

    public async Task<User?> UpdateUserAsync(UpdateUserForm formData)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == formData.Id);
        if (entity == null)
            return null;

        entity = UserMapper.ToEntity(formData) ?? entity;
        await _userManager.UpdateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == formData.Id);
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _userManager.DeleteAsync(entity);
        await UpdateCacheAsync();
        return true;
    }

    public async Task<IEnumerable<User>> UpdateCacheAsync()
    {
        var entities = await _userRepository.GetAllAsync();
        var models = entities.Select(UserMapper.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }


}
