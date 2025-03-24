namespace Entrance.Shared.Models;

#region REQUESTS
public class PhysicalHealthRequest
{
    public int ApplicantId { get; set; }
    public bool IsWithDisability { get; set; } = false;
    public string? Disability { get; set; }
    public string? ChronicIllnesses { get; set; }
    public string? Medicines { get; set; }
    public string? AccidentsExperienced { get; set; }
    public string? OperationsExperienced { get; set; }
    public string? PWDId { get; set; }
    public DateTime? DateIssued { get; set; }
    public DateTime? Expiration { get; set; }
}
#endregion


#region RESPONSES
public class PhysicalHealthResponse
{
    public int Id { get; set; }
    public bool IsWithDisability { get; set; }
    public string? Disability { get; set; }
    public string? ChronicIllnesses { get; set; }
    public string? Medicines { get; set; }
    public string? AccidentsExperienced { get; set; }
    public string? OperationsExperienced { get; set; }
    public string? PWDId { get; set; }
    public DateTime? DateIssued { get; set; }
    public DateTime? Expiration { get; set; }
}
#endregion
