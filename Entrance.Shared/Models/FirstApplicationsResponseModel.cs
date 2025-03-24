namespace Entrance.Shared.Models;

#region REQUESTS
public class FirstApplicationInfoRequest
{
    public int ApplicantId { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public string Track { get; set; }
    public string ApplicantStatus { get; set; }
    public DateTime TransactionDate { get; set; }
}
#endregion


#region RESPONSES
public class FirstApplicationInfoResponse
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public string Track { get; set; }
    public string ApplicantStatus { get; set; }
    public DateTime TransactionDate { get; set; }
}
#endregion
