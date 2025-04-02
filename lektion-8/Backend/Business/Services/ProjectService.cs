using Business.Dtos;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
namespace Business.Services;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(AddProjectFormData projectFormData);
    Task<bool> DeleteProjectAsync(string projectId);
    Task<Project?> GetProjectByIdAsync(string projectId);
    Task<IEnumerable<Project>?> GetProjectsAsync();
    Task<bool> UpdateProjectAsync(UpdateProjectFormData projectFormData);
}

public class ProjectService(ProjectRepository projectRepository, IMemoryCache cache) : IProjectService
{
    private readonly ProjectRepository _projectRepository = projectRepository;
    private readonly IMemoryCache _cache = cache;
    private const string _cacheKey_All = "Project_All";

    public async Task<bool> CreateProjectAsync(AddProjectFormData projectFormData)
    {
        if (projectFormData == null)
            return false;

        var entity = new ProjectEntity
        {
            ProjectName = projectFormData.ProjectName,
            Description = projectFormData.Description,
            StartDate = projectFormData.StartDate,
            EndDate = projectFormData.EndDate,
            Budget = projectFormData.Budget,
            ClientId = projectFormData.ClientId,
            UserId = projectFormData.UserId,
            StatusId = 1,
            Created = DateTime.Now
        };

        var result = await _projectRepository.AddAsync(entity);
        if (result)
        {
            _cache.Remove(_cacheKey_All);
        }

        return result;
    }


    public async Task<IEnumerable<Project>?> GetProjectsAsync()
    {
        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Project>? cachedItems))
            return cachedItems;

        var projects = await SetCache();
        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(string projectId)
    {
        var project = new Project();

        if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Project>? cachedItems))
        {
            project = cachedItems?.FirstOrDefault(x => x.Id == projectId);
            if (project != null)
                return project;
        }

        var entity = await _projectRepository.GetAsync(x => x.Id == projectId);
        if (entity == null)
            return null;

        await SetCache();

        project = new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            Created = entity.Created,
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
                Email = entity.User.Email!,
                PhoneNumber = entity.User.PhoneNumber,
                StreetName = entity.User.Address?.StreetName,
                PostalCode = entity.User.Address?.PostalCode,
                City = entity.User.Address?.City,
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            }
        };
        return project;
    }

    public async Task<bool> UpdateProjectAsync(UpdateProjectFormData projectFormData)
    {
        if (projectFormData == null)
            return false;

        var project = await _projectRepository.GetAsync(x => x.Id == projectFormData.Id);
        if (project == null)
            return false;



        var result = await _projectRepository.UpdateAsync(project);
        if (result)
        {
            _cache.Remove(_cacheKey_All);
        }

        return result;
    }

    public async Task<bool> DeleteProjectAsync(string projectId)
    {
        if (string.IsNullOrEmpty(projectId))
            return false;

        var project = await _projectRepository.GetAsync(x => x.Id == projectId);
        if (project == null)
            return false;

        var result = await _projectRepository.DeleteAsync(x => x.Id == projectId);
        if (result)
        {
            _cache.Remove(_cacheKey_All);
        }

        return result;
    }

    public async Task<IEnumerable<Project>> SetCache()
    {
        _cache.Remove(_cacheKey_All);
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(x => new Project
        {
            Id = x.Id,
            ProjectName = x.ProjectName,
            Description = x.Description,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Budget = x.Budget,
            Created = x.Created,
            Client = new Client
            {
                Id = x.Client.Id,
                ClientName = x.Client.ClientName
            },
            User = new User
            {
                Id = x.User.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email!,
                PhoneNumber = x.User.PhoneNumber,
                StreetName = x.User.Address?.StreetName,
                PostalCode = x.User.Address?.PostalCode,
                City = x.User.Address?.City,
            },
            Status = new Status
            {
                Id = x.Status.Id,
                StatusName = x.Status.StatusName
            }
        });
        projects = projects.OrderByDescending(x => x.Created);
        _cache.Set(_cacheKey_All, projects, TimeSpan.FromMinutes(10));

        return projects;
    }

}
