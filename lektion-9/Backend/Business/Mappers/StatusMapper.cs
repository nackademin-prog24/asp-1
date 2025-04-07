using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class StatusMapper
{
    public static StatusEntity ToEntity(AddStatusFormData? formData)
    {
        if (formData == null) return null!;
        return new StatusEntity { StatusName = formData.StatusName };
    }

    public static StatusEntity ToEntity(UpdateStatusFormData? formData)
    {
        if (formData == null) return null!;
        return new StatusEntity { Id = formData.Id,  StatusName = formData.StatusName };
    }

    public static Status ToModel(StatusEntity? entity)
    {
        if (entity == null) return null!;
        return new Status
        {
            Id = entity.Id,
            StatusName = entity.StatusName
        };
    }
}