using Entrance.Shared.Models;

namespace Entrance.Tests.ServiceTests;
public class InterviewResultShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformInterviewsMethods()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();
        var schedule = await SchedulesDefault();
        var courses = schedule.Data.List.Select(x => x.Campus.Courses).FirstOrDefault();

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

        // Act & Assert: Verify interview result was created
        var interviewModel = await Connect.InterviewResult.Get(interviewId);
        Assert.NotNull(interviewModel);
        Assert.Equal(interviewId, interviewModel.Data.Id);

        // Act & Assert: Verify applicant's interview result was recorded
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(interviewId, applicantModel.Data.Interviews.Select(x => x.Id));

        // Arrange: Update interview set to active
        var updateActiveModel = new InterviewActiveUpdate()
        {
            Id = interviewId,
            IsUse = true,
            UpdatedById = user.Data.Id
        };
        var updatedActiveModel = await Connect.InterviewResult.Activate(updateActiveModel);
        Assert.True(updatedActiveModel.IsSuccessful);

        // Act & Assert: Verify applicant's selected active result 
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(true, applicantModel.Data.Interviews.Select(x => x.IsUse));

        // Arrange: Update interview set to inactive
        updateActiveModel = new InterviewActiveUpdate()
        {
            Id = interviewId,
            IsUse = false,
            UpdatedById = user.Data.Id
        };
        updatedActiveModel = await Connect.InterviewResult.Activate(updateActiveModel);
        Assert.True(updatedActiveModel.IsSuccessful);

        // Act & Assert: Verify applicant's selected active result 
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(false, applicantModel.Data.Interviews.Select(x => x.IsUse));

        // Arrange: Update interview rating
        var updateRatingModel = new InterviewResultUpdate()
        {
            Id = interviewId,
            InterviewReading = 90,
            InterviewCommunication = 80,
            InterviewAnalytical = 90,
            Interviewer = "Mac Fly",
            UpdatedById = user.Data.Id
        };
        var updatedRatingModel = await Connect.InterviewResult.UpdateRating(updateRatingModel);
        Assert.True(updatedRatingModel.IsSuccessful);

        // Arrange: Create another Interview with the same course within the same campus, expect failure
        var existingInterview = new InterviewResultRequest
        {
            ApplicantId = applicant.Id,
            CourseId = courses.FirstOrDefault().Id,
            InterviewReading = 85,
            InterviewCommunication = 80,
            InterviewAnalytical = 80,
            InterviewDate = new DateTime(2024, 7, 4),
            Interviewer = "Mac Fly",
            RecordedById = user.Data.Id
        };
        var exceptionCreate = await Connect.InterviewResult.Create(existingInterview);

        // Assert: Check the exception message
        Assert.False(exceptionCreate.IsSuccessful);
        Assert.Equal("Applicant has already been interviewed in this program.", exceptionCreate.Messages.FirstOrDefault());

        // Arrange: Create another Interview 
        var interviewRequest2 = new InterviewResultRequest
        {
            ApplicantId = applicant.Id,
            CourseId = courses.LastOrDefault().Id,
            InterviewReading = 90,
            InterviewCommunication = 95,
            InterviewAnalytical = 85,
            InterviewDate = new DateTime(2024, 7, 4),
            Interviewer = "Mac Fly",
            RecordedById = user.Data.Id
        };

        var interviewResult2 = await Connect.InterviewResult.Create(interviewRequest2);
        Assert.True(interviewResult2.IsSuccessful);
        var interviewId2 = interviewResult2.Data;

        // Act & Assert: Verify another interview was created
        var interviewModel2 = await Connect.InterviewResult.Get(interviewId2);
        Assert.NotNull(interviewModel2);
        Assert.Equal(interviewId2, interviewModel2.Data.Id);

        // Act & Assert: Verify applicant's interview result was created
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(interviewId, applicantModel.Data.Interviews.Select(x => x.Id));
        Assert.Contains(interviewId2, applicantModel.Data.Interviews.Select(x => x.Id));
    }
}
