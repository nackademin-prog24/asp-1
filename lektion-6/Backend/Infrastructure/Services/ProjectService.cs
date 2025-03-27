using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;


public class ProjectService(IProjectRepository projectRepository, IStatusService statusService)
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;


    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var entites = await _projectRepository.GetAllAsync();
        var projects = entites.Select(entity => new Project
        {

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
