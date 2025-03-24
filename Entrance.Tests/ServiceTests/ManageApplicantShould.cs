using Entrance.Domain.Entities;
using Entrance.Shared.Models;

namespace Entrance.Tests.ServiceTests;
public class ManageApplicantShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformTransfers()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();
        var schedules = await SchedulesDefault();
        var courses = schedules.Data.List;

        //Get applicant
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data);

        //Get applicant old schedule id
        var oldScheduleId = applicantModel.Data.ScheduleId;
        Assert.Equal(oldScheduleId, applicantModel.Data.ScheduleId);

        // Arrange: transfer applicant to another schedule
        var applicantNewSchedule = new ApplicantTransfer()
        {
            Id = applicantModel.Data.Id,
            ScheduleId = schedules.Data.List.ToArray()[1].Id,
            CourseId = applicantModel.Data.CourseId
        };
        var applicantNewScheduleModel = await Connect.Applicant.UpdateTransfer(applicantNewSchedule);
        Assert.True(applicantNewScheduleModel.IsSuccessful);

        // Assert: Verify applicant's schedule have changed
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data);
        Assert.NotEqual(oldScheduleId, applicantModel.Data.ScheduleId);
    }

    [Fact]
    public async Task PerformUpdateEmergencyContact()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

        //Get applicant
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data);

        var updateEmergencyContact = new EmergencyContactUpdate()
        {
            Id = applicantModel.Data.EmergencyContact.Id,
            ApplicantId = applicantModel.Data.Id,
            Name = "New Contact Person",
            ContactNo = "09997778885",
            Address = "New Address",
            Relationship = "New Relationship"
        };

        var result = await Connect.Applicant.UpdateEmergencyContact(updateEmergencyContact);
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task PerformUpdateGWAStatusTrack()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

        //Get applicant
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data);

        var updateGwaStatusTrack = new ApplicantUpdateGwaStatusTrack()
        {
            Id = applicantModel.Data.Id,
            GWA = 87.80,
            ApplicantStatus = "New",
            Track = "HUMS"
        };

        var result = await Connect.Applicant.UpdateGWAStatusTrack(updateGwaStatusTrack);
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task PerformUpdateLRN()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

        //Get applicant
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data);

        var updateLrn = new ApplicantUpdateLrn()
        {
            Id = applicantModel.Data.Id,
            LRN = "112345678911"
        };

        var result = await Connect.Applicant.UpdateLRN(updateLrn);
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task PerformUpdatePersonalInformation()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

        //Get applicant
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data);

        var updatePersonalInformation = new PersonalInformationUpdate()
        {
            Id = applicantModel.Data.PersonalInformation.Id,
            ApplicantId = applicantModel.Data.Id,
            FirstName = "Johnny",
            MiddleName = "Quincy",
            LastName = "Adams",
            NickName = "John",
            Sex = "Male",
            PlaceOfBirth = "Catbalogan City",
            Citizenship = "Filipino",
            Email = "test@email.com",
            ContactNumber = "09998798521",
            DateofBirth = new DateTime(1990, 1, 1),
            HouseNo = "163",
            Street = "San Francisco St",
            Barangay = "08",
            Purok = "1",
            Municipality = "Catbalogan City",
            Province = "Samar",
            ZipCode = "6700",
            NameExtension = "Jr."
        };

        var result = await Connect.Applicant.UpdatePersonalInformation(updatePersonalInformation);
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task PerformUpdateStudentId()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

        //Get applicant
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data);

        var updateStudentId = new ApplicantUpdateStudentId()
        {
            Id = applicantModel.Data.Id,
            StudentId = "115-63-5"
        };

        var result = await Connect.Applicant.UpdateStudentId(updateStudentId);
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task PerformGetListOfApplicantsBySchedule()
    {
        var user = await LoginDefault();
        var schedules = await SchedulesDefault();
        var schedule = schedules.Data.List.FirstOrDefault();
        // Arrange: check for applicant for successfull registration
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 10,
            SortField = nameof(Applicant.PersonalInformation.LastName),
            SortDir = DataGridQuerySortDirection.Ascending
        };
        var Models = await Connect.Applicant.ListScheduleApplicants(listQuery, user.Data.Access, schedule.Id);
        Assert.True(Models.IsSuccessful);
        Assert.True(Models.Data.List.Any());
    }

    [Fact]
    public async Task PerformGetListOfPassers()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();
        var schedule = await SchedulesDefault();
        var courses = schedule.Data.List.Select(x => x.Campus.Courses).FirstOrDefault();
              
        // Arrange: Create Examination Result
        var examRequest = new ExaminationResultRequest
        {
            ApplicantId = applicant.Id,
            IntelligenceRawScore = 80,
            MathRawScore = 33,
            ReadingRawScore = 35,
            ScienceRawScore = 34,
            RecordedById = user.Data.Id
        };
        var examResult = await Connect.ExamResult.Create(examRequest);
        Assert.True(examResult.IsSuccessful);
        var examId = examResult.Data;

        // Act & Assert: Verify exam result was created
        var examModel = await Connect.ExamResult.Get(examId);
        Assert.NotNull(examModel);


        // Arrange: Create Interview Result
        var interviewRequest = new InterviewResultRequest
        {
            ApplicantId = applicant.Id,
            CourseId = courses.FirstOrDefault().Id,
            InterviewReading = 80,
            InterviewCommunication = 85,
            InterviewAnalytical = 85,
            InterviewDate = new DateTime(2024, 7, 4),
            Interviewer = "Mac Fly",
            RecordedById = user.Data.Id
        };
        var interviewResult = await Connect.InterviewResult.Create(interviewRequest);
        Assert.True(interviewResult.IsSuccessful);
        var interviewId = interviewResult.Data;

        // Arrange: Update interview set to active
        var updateActiveModel = new InterviewActiveUpdate()
        {
            Id = interviewId,
            IsUse = true,
            UpdatedById = user.Data.Id
        };
        var updatedActiveModel = await Connect.InterviewResult.Activate(updateActiveModel);
        Assert.True(updatedActiveModel.IsSuccessful);

        var gwa = new ApplicantUpdateGwaStatusTrack
        {
            Id = applicant.Id,
            GWA = 85.00,
            ApplicantStatus = applicant.ApplicantStatus,
            Track = applicant.Track            
        };

        var gwaResult = await Connect.Applicant.UpdateGWAStatusTrack(gwa);
        Assert.True(gwaResult.IsSuccessful);

        // Arrange: check for applicant for successfull registration
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 10,
            SortField = nameof(ApplicantPassersListResponse.PersonalInformation.LastName),
            SortDir = DataGridQuerySortDirection.Ascending
        };
        var Models = await Connect.Applicant.Passers(listQuery, user.Data.Access);
        Assert.True(Models.IsSuccessful);
        Assert.True(Models.Data.List.Any());
    }
}

