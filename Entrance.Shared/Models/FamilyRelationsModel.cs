﻿namespace Entrance.Shared.Models;

#region REQUESTS
public class FamilyRelationRequest
{
    public int ApplicantId { get; set; }
    public string? FullName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Sex { get; set; }
    public string? GradeCourse { get; set; }
    public string? SchoolOccupation { get; set; }
    public string? MonthlyIncome { get; set; }
}
#endregion


#region RESPONSES
public class FamilyRelationResponse
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Sex { get; set; }
    public string? GradeCourse { get; set; }
    public string? SchoolOccupation { get; set; }
    public string? MonthlyIncome { get; set; }
}
#endregion
