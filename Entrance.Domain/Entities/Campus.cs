using Entrance.Domain.Contracts;

namespace Entrance.Domain.Entities;
public class Campus : BaseEntity<int>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public bool HasDepartment { get; set; } = false;
    public ICollection<Department>? Departments { get; set; }
    public ICollection<Course> Courses { get; set; } 
    public ICollection<Schedule> Schedules { get; set; } 
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateUpdated { get; set; }
    public int CreatedById { get; set; }
    public int? UpdatedById { get; set; }

    public Campus Update(string name, string address, bool hasDepartment, int? updatedById)
    {
        Name = name;
        Address = address;
        HasDepartment = hasDepartment;
        DateUpdated = DateTime.Now;
        UpdatedById = updatedById;
        return this;
    }
}
