using AutoMapper;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;

namespace Entrance.Application.Mapping;
public class EntityMapping : Profile
{
	public EntityMapping()
	{
        #region APPLICANTS
        CreateMap<Applicant, ApplicantResponse>();       
        CreateMap<Applicant, ApplicantPersonalInformationResponse>();
        CreateMap<Applicant, ApplicantTransfer>();
        CreateMap<Applicant, ApplicantUpdateGwaStatusTrack>();
        CreateMap<Applicant, ApplicantUpdateStudentId>();
        CreateMap<Applicant, ApplicantUpdateLrn>();
        CreateMap<Applicant, ApplicantUpdateRegistered>();
        CreateMap<Applicant, ApplicantLrnResponse>();

        CreateMap<Applicant, ApplicantStudentInformationResponse>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Schedule.Campus.Courses.Where(x => x.Id == src.CourseId).Select(x => x.Name).FirstOrDefault()));

        CreateMap<Applicant, ApplicantPersonalInformationResponse>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Schedule.Campus.Courses.Where(x => x.Id == src.CourseId).Select(x => x.Name).FirstOrDefault()));

        CreateMap<Applicant, ApplicantApplicationResponse>()
            .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Schedule.ScheduleDate))
            .ForMember(dest => dest.ScheduleTime, opt => opt.MapFrom(src => src.Schedule.Time))
            .ForMember(dest => dest.ScheduleVenue, opt => opt.MapFrom(src => src.Schedule.Venue));

        CreateMap<Applicant, ApplicantStudentInformationResponse>()
			.ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Schedule.Campus.Courses.Where(x => x.Id == src.CourseId).Select(x => x.Name).FirstOrDefault()));

        CreateMap<Applicant, ApplicantListResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
        src.PersonalInformation != null
        ? $"{src.PersonalInformation.LastName} {src.PersonalInformation.FirstName} " +
          (string.IsNullOrEmpty(src.PersonalInformation.MiddleName) ? "" : src.PersonalInformation.MiddleName.Substring(0, 1))
        : string.Empty))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Schedule.Time))
            .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Schedule.Venue))
            .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Schedule.ScheduleDate))
            .ForMember(dest => dest.SchoolYear, opt => opt.MapFrom(src => src.Schedule.SchoolYear))
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Schedule.Campus.Courses.Where(x => x.Id == src.CourseId).Select(x => x.Name).FirstOrDefault()))
            .ForMember(dest => dest.CampusName, opt => opt.MapFrom(src => src.Schedule.Campus.Name));
           
        CreateMap<Applicant, ApplicantListScheduleResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                $"{src.PersonalInformation.LastName} " +
                $"{src.PersonalInformation.FirstName} " +
                (string.IsNullOrEmpty(src.PersonalInformation.MiddleName) ? "" : src.PersonalInformation.MiddleName.Substring(0, 1))))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.PersonalInformation.DateofBirth.ToString("MMMM dd, yyyy")))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Schedule.Time))
            .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Schedule.Venue))
            .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Schedule.ScheduleDate))
            .ForMember(dest => dest.SchoolYear, opt => opt.MapFrom(src => src.Schedule.SchoolYear));

        CreateMap<Applicant, ApplicantPassersListResponse>()                        
            .ForMember(dest => dest.SchoolYear, opt => opt.MapFrom(src => src.Schedule.SchoolYear));
        #endregion

        #region CAMPUSES
        CreateMap<Campus, CampusResponse>();
        CreateMap<Campus, CampusDetailsResponse>();
        CreateMap<Campus, CampusListResponse>();
        #endregion

        #region DEPARTMENTS
        CreateMap<Department, DepartmentResponse>();
        CreateMap<Department, DepartmentDetailsResponse>();
        CreateMap<Department, DepartmentListResponse>();
        #endregion

        #region COURSES
        CreateMap<Course, CourseResponse>();
        CreateMap<Course, CourseDetailsResponse>();
        CreateMap<Course, CourseListResponse>();
        #endregion

        #region SCHEDULES
        CreateMap<Schedule, ScheduleListResponse>()
            .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
            .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.ScheduleDate))
            .ForMember(dest => dest.Slot, opt => opt.MapFrom(src => src.Slot))
            .ForMember(dest => dest.AvailableSlot, opt => opt.MapFrom(src => (src.Slot - src.Applicants.Count(x => x.Registered == true))))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time))
            .ForMember(dest => dest.SchoolYear, opt => opt.MapFrom(src => src.SchoolYear));
        CreateMap<Schedule, ScheduleDetailsResponse>();
        CreateMap<Schedule, ScheduleResponse>();
        #endregion

        #region REGISTRATIONS
        CreateMap<FirstApplicationInfo, FirstApplicationInfoResponse>();
        CreateMap<PersonalInformation, PersonalInformationResponse>();
        CreateMap<PersonalInformation, PersonalInformationUpdate>();
        CreateMap<AcademicBackground, AcademicBackgroundResponse>();
        CreateMap<Spouse, SpouseResponse>();
        CreateMap<PhysicalHealth, PhysicalHealthResponse>();
        CreateMap<CounselorConsultation, CounselorConsultationResponse>();
        CreateMap<PsychiatristConsultation, PsychiatristConsultationResponse>();
        CreateMap<PsychologistConsultation, PsychologistConsultationResponse>();
        CreateMap<FamilyRelation, FamilyRelationResponse>();
        CreateMap<ParentGuardianInformation, ParentGuardianInformationResponse>();
        CreateMap<EmergencyContact, EmergencyContactResponse>();
        CreateMap<EmergencyContact, EmergencyContactUpdate>();
        CreateMap<PersonalityProfile, PersonalityProfileResponse>();
        CreateMap<Transfer, TransferResponse>();
        CreateMap<SoloParent, SoloParentResponse>();
        #endregion

        #region EXAMINATIONS
        CreateMap<Examination, ExaminationResponse>();
        CreateMap<Examination, ExaminationResultUpdate>();
        #endregion

        #region INTERVIEWS
        CreateMap<Interview, InterviewResponse>();
        CreateMap<Interview, InterviewResultUpdate>();
        CreateMap<Interview, InterviewActiveUpdate>();
        #endregion
    }
}
