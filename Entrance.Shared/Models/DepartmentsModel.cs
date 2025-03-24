using Entrance.Shared.Validator;
using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class DepartmentRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [IdValidator(ErrorMessage = "Campus is required")]
    public int CampusId { get; set; }
    public int CreatedById { get; set; }
}

public class DepartmentUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is require.")]
    public string Name { get; set; }
    [IdValidator(ErrorMessage = "Campus is required")]
    public int CampusId { get; set; }
    public int UpdatedById { get; set; }
}
#endregion

#region RESPONSES
public class DepartmentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CampusResponse Campus { get; set; }
    public ICollection<CourseListResponse> Courses { get; set; }
}
public class DepartmentDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CampusName { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}

public class DepartmentListResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
#endregion