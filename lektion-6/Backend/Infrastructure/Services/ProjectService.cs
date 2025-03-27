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
            Image = entity.Image,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            }
        });

        return projects;
    }


    public async Task<IEnumerable<Project>> GetProjectsByClientIdAsync(string clientId)
    {
        var entites = await _projectRepository.GetAllAsync(
            orderByDescending: true,
            sortBy: x => x.Created,
            filterBy: x => x.ClientId == clientId,
            i => i.User,
            i => i.Client,
            i => i.Status
        );

        var projects = entites.Select(entity => new Project
        {
            Id = entity.Id,
            Image = entity.Image,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            }
        });

        return projects;
    }


    public async Task<Project> GetProjectByIdAsync(string id)
    {
        var entity = await _projectRepository.GetAsync(
          
            x => x.Id == id,
            i => i.User,
            i => i.Client,
            i => i.Status
        );
        
        return entity == null ? null! : new Project
        {
            Id = entity.Id,
            Image = entity.Image,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            }
        };
    }

    public async Task<Project> GetProjectByProjectNameAsync(string projectName)
    {
        var entity = await _projectRepository.GetAsync(

            x => x.ProjectName == projectName,
            i => i.User,
            i => i.Client,
            i => i.Status
        );

        return entity == null ? null! : new Project
        {
            Id = entity.Id,
            Image = entity.Image,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            }
        };
    }
}
