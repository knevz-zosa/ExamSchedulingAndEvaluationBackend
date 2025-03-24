using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class EmergencyContactRequest
{
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "Contact person name is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Contact person number is required.")]
    public string ContactNo { get; set; }
    [Required(ErrorMessage = "Contact person address is required.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Relationship to contact is required.")]
    public string Relationship { get; set; }
}

public class EmergencyContactUpdate
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "Contact person name is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Contact person number is required.")]
    public string ContactNo { get; set; }
    [Required(ErrorMessage = "Contact person address is required.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Relationship to contact is required.")]
    public string Relationship { get; set; }
}

#endregion


#region RESPONSES
public class EmergencyContactResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactNo { get; set; }
    public string Address { get; set; }
    public string Relationship { get; set; }
}
#endregion
