using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class UserMapper
{
    public static UserEntity ToEntity(AddUserFormData? formData)
    {
        if (formData == null) return null!;
        return new UserEntity 
        { 
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            Email = formData.Email
        };
    }

    public static UserEntity ToEntity(UpdateUserFormData? formData, string? imageFileName = null)
    {
        if (formData == null) return null!;
        return new UserEntity
        {
            Id = formData.Id,
            ImageFileName = imageFileName,
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            Email = formData.Email,
            PhoneNumber = formData.PhoneNumber,
            StreetName = formData.StreetName,
            PostalCode = formData.PostalCode,
            City = formData.City
        };
    }

    public static User ToModel(UserEntity? entity)
    {
        if (entity == null) return null!;
        return new User
        {
            Id = entity.Id,
            ImageFileName = entity.ImageFileName,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            PhoneNumber = entity.PhoneNumber,
            StreetName = entity.StreetName,
            PostalCode = entity.PostalCode,
            City = entity.City
        };
    }
}