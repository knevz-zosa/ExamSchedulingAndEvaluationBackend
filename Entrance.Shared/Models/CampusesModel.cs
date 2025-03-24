using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class CampusRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    public bool HasDepartment { get; set; } = false;
    public int CreatedById { get; set; }
}

public class CampusUpdate
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    public bool HasDepartment { get; set; } = false;
    public int UpdatedById { get; set; }
}
#endregion

#region RESPONSES
public class CampusResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<DepartmentListResponse>? Departments { get; set; }
    public ICollection<CourseListResponse> Courses { get; set; }
    public ICollection<ScheduleListResponse> Schedules { get; set; }
}

public class CampusDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public bool HasDepartment { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}

public class CampusListResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
#endregion