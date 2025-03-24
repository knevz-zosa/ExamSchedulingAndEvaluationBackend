using Entrance.Domain.Contracts;

namespace Entrance.Domain.Entities;
public class Interview : BaseEntity<int>
{
    public DateTime InterviewDate { get; set; }
    public int CourseId { get; set; }
    public int InterviewReading { get; set; }
    public int InterviewCommunication { get; set; }
    public int InterviewAnalytical { get; set; }
    public int ApplicantId { get; set; }
    public bool IsUse { get; set; }
    public Applicant Applicant { get; set; }
    public DateTime DateRecorded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public int RecordedById { get; set; }
    public int UpdatedById { get; set; }
    public string Interviewer { get; set; }
    public Interview UpdateRating(int interviewReading, int interviewCommunication, int interviewAnalytical, int updatedById, string interviewer)
    {
        InterviewReading = interviewReading;
        InterviewCommunication = interviewCommunication;
        InterviewAnalytical = interviewAnalytical;
        DateUpdated = DateTime.Now;
        UpdatedById = updatedById;
        Interviewer = interviewer;
        return this;
    }

    public Interview UpdateIsActive(bool isUse, int updatedById)
    {
        IsUse = isUse;
        DateUpdated = DateTime.Now;
        UpdatedById = updatedById;
        return this;
    }
}
