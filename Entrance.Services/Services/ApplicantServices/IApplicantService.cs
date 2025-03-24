using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.ApplicantServices;
public interface IApplicantService
{
    Task<ResponseWrapper<int>> Transfer(ApplicantRequest request);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<int>> UpdateEmergencyContact(EmergencyContactUpdate update);
    Task<ResponseWrapper<int>> UpdateGWAStatusTrack(ApplicantUpdateGwaStatusTrack update);
    Task<ResponseWrapper<int>> UpdateLRN(ApplicantUpdateLrn update);
    Task<ResponseWrapper<int>> UpdatePersonalInformation(PersonalInformationUpdate update);
    Task<ResponseWrapper<int>> UpdateTransfer(ApplicantTransfer update);
    Task<ResponseWrapper<int>> UpdateStudentId(ApplicantUpdateStudentId update);
    Task<ResponseWrapper<int>> UpdateRegistered(ApplicantUpdateRegistered update);
    Task<ResponseWrapper<ApplicantResponse>> Get(int id);
    Task<ResponseWrapper<ApplicantApplicationResponse>> GetApplicantApplication(int id);
    Task<ResponseWrapper<ApplicantPersonalInformationResponse>> GetApplicantPersonalInformation(int id);
    Task<ResponseWrapper<ApplicantStudentInformationResponse>> GetApplicantStudentInformation(int id);
    Task<ResponseWrapper<FirstApplicationInfoResponse>> GetFirstApplication(int id);
    Task<ResponseWrapper<PersonalInformationResponse>> GetPersonalInformation(int id);
    Task<ResponseWrapper<ApplicantLrnResponse>> GetLRN(int id);
    Task<ResponseWrapper<PagedList<ApplicantListResponse>>> List(DataGridQuery query, string access);
    Task<ResponseWrapper<PagedList<ApplicantListScheduleResponse>>> ListScheduleApplicants (DataGridQuery query, string access, int schedId);
    Task<ResponseWrapper<PagedList<ApplicantInprogressListResponse>>> ListInProgress(DataGridQuery query, string access);
    Task<ResponseWrapper<PagedList<ApplicantApplicationResponse>>> Passers(DataGridQuery query, string access);
}
