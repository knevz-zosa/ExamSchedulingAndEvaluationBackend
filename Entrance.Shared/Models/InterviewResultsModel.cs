using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class InterviewResultRequest
{
    public DateTime InterviewDate { get; set; }
    public int CourseId { get; set; }
    public int InterviewReading { get; set; }

    public int InterviewCommunication { get; set; }

    public int InterviewAnalytical { get; set; }
    public int ApplicantId { get; set; }
    public bool IsUse { get; set; } = false;
    public DateTime DateRecorded { get; set; } = DateTime.Now;
    public int RecordedById { get; set; }
    [Required(ErrorMessage = "Interview name is required")]
    public string Interviewer { get; set; }
}

public class InterviewResultUpdate
{
    public int Id { get; set; }
    public int InterviewReading { get; set; }
    public int InterviewCommunication { get; set; }
    public int InterviewAnalytical { get; set; }
    public int UpdatedById { get; set; }
    [Required(ErrorMessage = "Interview name is required")]
    public string Interviewer { get; set; }
}

public class InterviewActiveUpdate
{
    public int Id { get; set; }
    public bool IsUse { get; set; }
    public int UpdatedById { get; set; }
}
#endregion


#region RESPONSES
public class InterviewResponse
{
    public int Id { get; set; }
    public DateTime InterviewDate { get; set; }
    public int CourseId { get; set; }
    public int InterviewReading { get; set; }
    public int InterviewCommunication { get; set; }
    public int InterviewAnalytical { get; set; }
    public bool IsUse { get; set; }
    public DateTime DateRecorded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string RecordedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string Interviewer { get; set; }
}
#endregion