namespace Entrance.Shared.Models;

#region REQUESTS
public class SoloParentRequest
{
    public int ApplicantId { get; set; }
    public string? SoloParentId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
#endregion


#region RESPONSES
public class SoloParentResponse
{
    public int Id { get; set; }
    public string? SoloParentId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
#endregion
