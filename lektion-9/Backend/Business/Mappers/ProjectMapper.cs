﻿using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class ProjectMapper
{
    public static ProjectEntity ToEntity(AddProjectFormData? formData, string? imageFileName = null)
    {
        if (formData == null) return null!;
        return new ProjectEntity 
        {
            ImageFileName = imageFileName,
            ProjectName = formData.ProjectName,
            ClientId = formData.ClientId,
            Description = formData.Description,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            UserId = formData.UserId,
        };
    }

    public static ProjectEntity ToEntity(UpdateProjectFormData? formData, string? imageFileName = null)
    {
        if (formData == null) return null!;
        return new ProjectEntity
        {
            Id = formData.Id,
            ImageFileName = imageFileName ?? formData.ExistingImageFileName,
            ProjectName = formData.ProjectName,
            ClientId = formData.ClientId,
            Description = formData.Description,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            UserId = formData.UserId,
            StatusId = formData.StatusId
        };
    }

    public static Project ToModel(ProjectEntity? entity)
    {
        if (entity == null) return null!;
        return new Project
        {
            Id = entity.Id,
            ImageFileName = entity.ImageFileName,
            ProjectName = entity.ProjectName,
            Client = ClientMapper.ToModel(entity.Client),
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            User = UserMapper.ToModel(entity.User),
            Status = StatusMapper.ToModel(entity.Status),
        };
    }
}
