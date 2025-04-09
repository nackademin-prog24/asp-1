using Business.Dtos;
using Data.Entities;

namespace Business.Mappers;


public static class AuthMapper
{
    public static UserEntity MapToEntity(SignUpDto dto)
    {
        if (dto == null) return null!;

        return new UserEntity
        {
            UserName = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
        };
    }
}
