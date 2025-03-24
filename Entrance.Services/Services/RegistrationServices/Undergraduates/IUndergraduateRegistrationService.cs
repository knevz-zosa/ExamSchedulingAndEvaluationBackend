using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.RegistrationServices.Undergraduates;
public interface IUndergraduateRegistrationService
{
    Task<ResponseWrapper<int>> Application(ApplicantRequest request);
    Task<ResponseWrapper<int>> FirstApplication(FirstApplicationInfoRequest request);
    Task<ResponseWrapper<int>> PersonalInformation(PersonalInformationRequest request);
    Task<ResponseWrapper<int>> Spouse(SpouseRequest request);
    Task<ResponseWrapper<int>> SoloParent(SoloParentRequest request);
    Task<ResponseWrapper<int>> Academic(AcademicBackgroundRequest request);
    Task<ResponseWrapper<int>> ParentsGuardian(ParentGuardianInformationRequest request);
    Task<ResponseWrapper<int>> Family(FamilyRelationRequest request);
    Task<ResponseWrapper<int>> Psychiatrist(PsychiatristConsultationRequest request);
    Task<ResponseWrapper<int>> Psychologist(PsychologistConsultationRequest request);
    Task<ResponseWrapper<int>> Counselor(CounselorConsultationRequest request);
    Task<ResponseWrapper<int>> PhysicalHealth(PhysicalHealthRequest request);
    Task<ResponseWrapper<int>> Personality(PersonalityProfileRequest request);
    Task<ResponseWrapper<int>> EmergencyContact(EmergencyContactRequest request);
}
