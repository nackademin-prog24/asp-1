using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities;

public class StatusEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}