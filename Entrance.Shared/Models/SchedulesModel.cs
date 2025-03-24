using Entrance.Shared.Validator;
using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class ScheduleRequest
{
    [Required(ErrorMessage = "Schedule date is required")]
    public DateTime ScheduleDate { get; set; }
    [Required(ErrorMessage = "Venue is required")]
    public string Venue { get; set; }
    [Required(ErrorMessage = "Time year is required")]
    public string Time { get; set; }
    public int Slot { get; set; }
    [IdValidator(ErrorMessage = "Campus is required")]
    public int CampusId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public int CreatedById { get; set; }
}
#endregion

#region RESPONSES

public class ScheduleResponse
{
    public int Id { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string Venue { get; set; }
    public string Time { get; set; }
    public int Slot { get; set; }
    public CampusResponse Campus { get; set; }
    public ICollection<ApplicantListResponse> Applicants { get; set; }
    public DateTime DateCreated { get; set; }
}

public class ScheduleDetailsResponse
{
    public int Id { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string Venue { get; set; }
    public string Time { get; set; }
    public int Slot { get; set; }
    public string CampusName { get; set; }
    public DateTime DateCreated { get; set; }
    public string CreatedBy { get; set; }
}

public class ScheduleListResponse
{   
    public int Id { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string Venue { get; set; }
    public string Time { get; set; }
    public int Slot { get; set; }
    public int AvailableSlot { get; set; }
    public CampusResponse Campus { get; set; }
    public ICollection<ApplicantListResponse> Applicants { get; set; }
}

#endregion
