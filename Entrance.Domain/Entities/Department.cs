using Entrance.Domain.Contracts;

namespace Entrance.Domain.Entities;
public class Department : BaseEntity<int>
{
    public string Name { get; set; }
    public int CampusId { get; set; }
    public Campus Campus { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateUpdated { get; set; }
    public int CreatedById { get; set; }
    public int? UpdatedById { get; set; }

    public Department Update(int campusId, string name, int? updatedById)
    {
        Name = name;
        CampusId = campusId;
        DateUpdated = DateTime.Now;
        UpdatedById = updatedById;
        return this;
    }
}