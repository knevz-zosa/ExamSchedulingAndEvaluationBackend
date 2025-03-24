using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;

#region REQUESTS
public class ApplicantRequest
{
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    [Required(ErrorMessage = "Applicant status is required.")]
    public string ApplicantStatus { get; set; }
    [Required(ErrorMessage = "Track is required.")]
    public string Track { get; set; }
    [Required(ErrorMessage = "LRN is required")]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "LRN must be exactly 12 digits")]
    public string LRN { get; set; }
    [Required(ErrorMessage = "Time is required.")]
    public DateTime TransactionDate { get; set; } = DateTime.Now;
}

public class ApplicantTransfer
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
}

public class ApplicantUpdateGwaStatusTrack
{
    public int Id { get; set; }
    public double? GWA { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
}

public class ApplicantUpdateStudentId
{
    public int Id { get; set; }
    public string? StudentId { get; set; }
}

public class ApplicantUpdateLrn
{
    public int Id { get; set; }
    public string LRN { get; set; }
}

public class ApplicantUpdateRegistered
{
    public int Id { get; set; }
    public bool Registered { get; set; }
}
#endregion

#region RESPONSES
public class ApplicantResponse
{
    public int Id { get; set; }
    public string ControlNo { get; set; }
    public DateTime TransactionDate { get; set; }
    public double? GWA { get; set; }
    public string LRN { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public string? StudentId { get; set; }
    public bool Registered { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public ScheduleResponse Schedule { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public SpouseResponse? Spouse { get; set; }
    public SoloParentResponse? SoloParent { get; set; }
    public AcademicBackgroundResponse AcademicBackground { get; set; }
    public ParentGuardianInformationResponse ParentGuardianInformation { get; set; }
    public PersonalityProfileResponse PersonalityProfile { get; set; }
    public PhysicalHealthResponse PhysicalHealth { get; set; }
    public PsychiatristConsultationResponse? PsychiatristConsultation { get; set; }
    public CounselorConsultationResponse? CouncelorConsultation { get; set; }
    public PsychologistConsultationResponse? PsychologistConsultation { get; set; }
    public EmergencyContactResponse EmergencyContact { get; set; }
    public ICollection<FamilyRelationResponse> FamilyRelations { get; set; }
    public ICollection<InterviewResponse>? Interviews { get; set; }
    public ICollection<TransferResponse>? Transfers { get; set; }
    public ExaminationResponse? Examination { get; set; }
    public FirstApplicationInfoResponse FirstApplicationInfo { get; set; }
}

public class ApplicantLrnResponse
{
    public int Id { get; set; }
    public string LRN { get; set; }
}

public class ApplicantStudentInformationResponse
{
    public int Id { get; set; }
    public string LRN { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public string? StudentId { get; set; }
    public bool Registered { get; set; }
    public string CourseName { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public SpouseResponse? Spouse { get; set; }
    public SoloParentResponse? SoloParent { get; set; }
    public AcademicBackgroundResponse AcademicBackground { get; set; }
    public ParentGuardianInformationResponse ParentGuardianInformation { get; set; }
    public PersonalityProfileResponse PersonalityProfile { get; set; }
    public PhysicalHealthResponse PhysicalHealth { get; set; }
    public PsychiatristConsultationResponse? PsychiatristConsultation { get; set; }
    public CounselorConsultationResponse? CouncelorConsultation { get; set; }
    public PsychologistConsultationResponse? PsychologistConsultation { get; set; }
}

public class ApplicantPersonalInformationResponse
{
    public int Id { get; set; }
    public double? GWA { get; set; }
    public string LRN { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public string? StudentId { get; set; }
    public bool Registered { get; set; }
    public string CourseName { get; set; }
    public string CourseTrimmedName { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public SpouseResponse? Spouse { get; set; }
    public AcademicBackgroundResponse AcademicBackground { get; set; }
    public ParentGuardianInformationResponse ParentGuardianInformation { get; set; }
}
public class ApplicantApplicationResponse
{
    public int Id { get; set; }
    public string ControlNo { get; set; }
    public DateTime TransactionDate { get; set; }
    public double? GWA { get; set; }
    public string LRN { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public bool Registered { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public string ScheduleDate { get; set; }
    public string ScheduleTime { get; set; }
    public string ScheduleVenue { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public AcademicBackgroundResponse AcademicBackground { get; set; }
    public ParentGuardianInformationResponse ParentGuardianInformation { get; set; }
    public InterviewResponse? Interview { get; set; }
    public ExaminationResponse? Examination { get; set; }
}

public class ApplicantListResponse
{
    public int Id { get; set; }
    public string Time { get; set; }
    public string Venue { get; set; }
    public string ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public string CourseName { get; set; }
    public string CampusName { get; set; }
    public bool Registered { get; set; }
    public string LRN { get; set; }
    public string FullName { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public ICollection<InterviewResponse>? Interviews { get; set; }
    public ExaminationResponse? Examination { get; set; }
}

public class ApplicantListScheduleResponse
{
    public int Id { get; set; }
    public string Time { get; set; }
    public string Venue { get; set; }
    public string ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string CourseName { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }

    public string CampusName { get; set; }
    public bool Registered { get; set; }
    public string LRN { get; set; }
    public string FullName { get; set; }
    public int ScheduleId { get; set; }
    public string DateOfBirth { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public ICollection<InterviewResponse>? Interviews { get; set; }
    public ExaminationResponse? Examination { get; set; }
}

public class ApplicantInprogressListResponse
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string Time { get; set; }
    public string Venue { get; set; }
    public string ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string CourseName { get; set; }
    public string CampusName { get; set; }
    public bool Registered { get; set; }
    public string FullName { get; set; }
    public string DateOfBirth { get; set; }
}

public class ApplicantPassersListResponse
{
    public int Id { get; set; }
    public double? GWA { get; set; }
    public string LRN { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public string? StudentId { get; set; }
    public bool Registered { get; set; }
    public string SchoolYear { get; set; }
    public int CourseId { get; set; }
    public string Remarks { get; set; }
    public CampusResponse Campus { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public SpouseResponse? Spouse { get; set; }
    public AcademicBackgroundResponse AcademicBackground { get; set; }
    public ParentGuardianInformationResponse ParentGuardianInformation { get; set; }
    public ICollection<InterviewResponse>? Interviews { get; set; }
    public ExaminationResponse? Examination { get; set; }
}
#endregion
