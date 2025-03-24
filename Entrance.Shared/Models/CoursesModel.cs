using Entrance.Shared.Validator;
using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class CourseRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public int? DepartmentId { get; set; }
    [IdValidator(ErrorMessage = "Campus is required")]
    public int CampusId { get; set; }
    public bool IsOpen { get; set; } = true;
    public int CreatedById { get; set; }
}

public class CourseUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public int? DepartmentId { get; set; }
    [IdValidator(ErrorMessage = "Campus is required")]
    public int CampusId { get; set; }
    public bool IsOpen { get; set; } = false;
    public int UpdatedById { get; set; }
}
#endregion


#region RESPONSES
public class CourseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DepartmentResponse? Department { get; set; }
    public CampusResponse Campus {get;set;}
}

public class CourseDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? DepartmentName { get; set; }
    public string CampusName { get; set; }
    public bool IsOpen { get; set; } = false;
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
public class CourseListResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
#endregion
