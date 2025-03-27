using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;


public class ProjectService(IProjectRepository projectRepository, IStatusService statusService)
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;


    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var entites = await _projectRepository.GetAllAsync(
            orderByDescending: true, 
            sortBy: x => x.Created, 
            filterBy: null, 
            i => i.User,
            i => i.Client,
            i => i.Status
        );

        var projects = entites.Select(entity => new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName
        });

        return projects;
    }

    public async Task<Project> GetProjectByIdAsync(string id)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == id);
        return entity == null ? null! : new Project
        {

        };
    }
}
