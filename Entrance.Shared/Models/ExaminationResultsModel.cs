using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class ExaminationResultRequest
{
    [Required(ErrorMessage = "Scores in reading is required.")]
    public int ReadingRawScore { get; set; }
    [Required(ErrorMessage = "Scores in math is required.")]
    public int MathRawScore { get; set; }
    [Required(ErrorMessage = "Scores in science is required.")]
    public int ScienceRawScore { get; set; }
    [Required(ErrorMessage = "Scores in intelligence test is required.")]
    public int IntelligenceRawScore { get; set; }
    public DateTime DateRecorded { get; set; } = DateTime.Now;
    public int RecordedById { get; set; }
    public int ApplicantId { get; set; }
}

public class ExaminationResultUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Scores in reading is required.")]
    public int ReadingRawScore { get; set; }
    [Required(ErrorMessage = "Scores in math is required.")]
    public int MathRawScore { get; set; }
    [Required(ErrorMessage = "Scores in science is required.")]
    public int ScienceRawScore { get; set; }
    [Required(ErrorMessage = "Scores in intelligence test is required.")]
    public int IntelligenceRawScore { get; set; }
    public int UpdatedById { get; set; }
}
#endregion


#region RESPONSES
public class ExaminationResponse
{
    public int Id { get; set; }
    public int ReadingRawScore { get; set; }
    public int MathRawScore { get; set; }
    public int ScienceRawScore { get; set; }
    public int IntelligenceRawScore { get; set; }
    public DateTime DateRecorded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string RecordedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
#endregion
