using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class ParentGuardianInformationRequest
{
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "Father's first name is required.")]
    public string FatherFirstName { get; set; }
    public string FatherMiddleName { get; set; }
    [Required(ErrorMessage = "Father's last name is required.")]
    public string FatherLastName { get; set; }
    public string? FatherContactNo { get; set; }
    [Required(ErrorMessage = "Father's citizenship is required.")]
    public string FatherCitizenship { get; set; }
    public DateTime FatherBirthday { get; set; }

    [Required(ErrorMessage = "Father's birth of place is required.")]
    public string FatherBirthPlace { get; set; }

    [Required(ErrorMessage = "Father's religion is required.")]
    public string FatherReligion { get; set; }

    [Required(ErrorMessage = "Father's marital is required.")]
    public string FatherMaritalStatus { get; set; }

    [Required(ErrorMessage = "Father's dialect is required.")]
    public string FatherDialect { get; set; }


    [Required(ErrorMessage = "Father's permanent address is required.")]
    public string FatherPermanentAddress { get; set; }

    [Required(ErrorMessage = "Father's educational attainment is required.")]
    public string FatherEducation { get; set; }


    [Required(ErrorMessage = "Father's estimated monthly income is required.")]
    public string FatherEstimatedMonthly { get; set; }

    [Required(ErrorMessage = "Father's other income is required.")]
    public string FatherOtherIncome { get; set; }
    public string? FatherEmail { get; set; }
    public string? FatherOccupation { get; set; }
    [Required(ErrorMessage = "Mother's first name is required.")]
    public string MotherFirstName { get; set; }
    public string MotherMiddleName { get; set; }
    [Required(ErrorMessage = "Mother's is last name required.")]
    public string MotherLastName { get; set; }
    public string? MotherContactNo { get; set; }
    [Required(ErrorMessage = "Mother's citizenship is required.")]
    public string MotherCitizenship { get; set; }
    public DateTime MotherBirthday { get; set; }

    [Required(ErrorMessage = "TMother's place of birth is required.")]
    public string MotherBirthPlace { get; set; }

    [Required(ErrorMessage = "Mother's religion is required.")]
    public string MotherReligion { get; set; }

    [Required(ErrorMessage = "Mother's marital status is required.")]
    public string MotherMaritalStatus { get; set; }

    [Required(ErrorMessage = "Mother's dialect is required.")]
    public string MotherDialect { get; set; }

    [Required(ErrorMessage = "Mother's permanent address is required.")]
    public string MotherPermanentAddress { get; set; }

    [Required(ErrorMessage = "Mother's educational attainment is required.")]
    public string MotherEducation { get; set; }

    [Required(ErrorMessage = "Mother's estimated monthly is required.")]
    public string MotherEstimatedMonthly { get; set; }

    [Required(ErrorMessage = "Mother's other income is required.")]
    public string MotherOtherIncome { get; set; }
    public string? MotherEmail { get; set; }
    public string? MotherOccupation { get; set; }
    [Required(ErrorMessage = "Guardian's first name is required.")]
    public string GuardianFirstName { get; set; }
    [Required(ErrorMessage = "Guardian's middle name is required.")]
    public string GuardianMiddleName { get; set; }
    [Required(ErrorMessage = "Guardian's last name is required.")]
    public string GuardianLastName { get; set; }
    public string? GuardianContactNo { get; set; }
    [Required(ErrorMessage = "Guardian's citizenship is required.")]
    public string GuardianCitizenship { get; set; }
    public string? GuardianEmail { get; set; }
    public string? GuardianOccupation { get; set; }
    public DateTime GuardianBirthday { get; set; }

    [Required(ErrorMessage = "Guardian's place of birth is required.")]
    public string GuardianBirthPlace { get; set; }

    [Required(ErrorMessage = "Guardian's religion is required.")]
    public string GuardianReligion { get; set; }

    [Required(ErrorMessage = "Guardian's marital status is required.")]
    public string GuardianMaritalStatus { get; set; }

    [Required(ErrorMessage = "Guardian's dialect is required.")]
    public string GuardianDialect { get; set; }

    [Required(ErrorMessage = "Guardian's permanent address is required.")]
    public string GuardianPermanentAddress { get; set; }

    [Required(ErrorMessage = "Guardian's educational attainment is required.")]
    public string GuardianEducation { get; set; }

    [Required(ErrorMessage = "Guardian's estimated monthly income is required.")]
    public string GuardianEstimatedMonthly { get; set; }

    [Required(ErrorMessage = "Guardian's other income is required.")]
    public string GuardianOtherIncome { get; set; }
}
#endregion


#region RESPONSES
public class ParentGuardianInformationResponse
{
    public int Id { get; set; }
    public string FatherFirstName { get; set; }
    public string? FatherMiddleName { get; set; }
    public string FatherLastName { get; set; }
    public string? FatherContactNo { get; set; }
    public string FatherCitizenship { get; set; }
    public string? FatherEmail { get; set; }
    public string? FatherOccupation { get; set; }
    public DateTime FatherBirthday { get; set; }
    public string FatherBirthPlace { get; set; }
    public string FatherReligion { get; set; }
    public string FatherMaritalStatus { get; set; }
    public string FatherDialect { get; set; }
    public string FatherPermanentAddress { get; set; }
    public string FatherEducation { get; set; }
    public string FatherEstimatedMonthly { get; set; }
    public string FatherOtherIncome { get; set; }
    public string MotherFirstName { get; set; }
    public string? MotherMiddleName { get; set; }
    public string MotherLastName { get; set; }
    public string? MotherContactNo { get; set; }
    public string MotherCitizenship { get; set; }
    public string? MotherEmail { get; set; }
    public string? MotherOccupation { get; set; }
    public DateTime MotherBirthday { get; set; }
    public string MotherBirthPlace { get; set; }
    public string MotherReligion { get; set; }
    public string MotherMaritalStatus { get; set; }
    public string MotherDialect { get; set; }
    public string MotherPermanentAddress { get; set; }
    public string MotherEducation { get; set; }
    public string MotherEstimatedMonthly { get; set; }
    public string MotherOtherIncome { get; set; }
    public string GuardianFirstName { get; set; }
    public string? GuardianMiddleName { get; set; }
    public string GuardianLastName { get; set; }
    public string? GuardianContactNo { get; set; }
    public string GuardianCitizenship { get; set; }
    public string? GuardianEmail { get; set; }
    public string? GuardianOccupation { get; set; }
    public DateTime GuardianBirthday { get; set; }
    public string GuardianBirthPlace { get; set; }
    public string GuardianReligion { get; set; }
    public string GuardianMaritalStatus { get; set; }
    public string GuardianDialect { get; set; }
    public string GuardianPermanentAddress { get; set; }
    public string GuardianEducation { get; set; }
    public string GuardianEstimatedMonthly { get; set; }
    public string GuardianOtherIncome { get; set; }
}
#endregion
