namespace Entrance.Shared.Models;

#region REQUESTS
public class PsychologistConsultationRequest
{
    public int ApplicantId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int? Sessions { get; set; }
    public string? Reasons { get; set; }
}
#endregion


#region RESPONSES
public class PsychologistConsultationResponse
{
    public int Id { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int? Sessions { get; set; }
    public string? Reasons { get; set; }
}
#endregion
