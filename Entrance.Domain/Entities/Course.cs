using Entrance.Domain.Contracts;

namespace Entrance.Domain.Entities;
public class Course : BaseEntity<int>
{
    public string Name { get; set; }
    public int? DepartmentId { get; set; }
    public int CampusId { get; set; }
    public Campus Campus { get; set; }
    public bool IsOpen { get; set; } = true;
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public int CreatedById { get; set; }
    public int? UpdatedById { get; set; }


    public Course Update(int campusId, int? departmentId, string name, int? updatedById, bool isOpen)
    {
        Name = name;
        CampusId = campusId;
        DepartmentId = departmentId;
        IsOpen = isOpen;
        DateUpdated = DateTime.Now;
        UpdatedById = updatedById;
        return this;
    }
}
