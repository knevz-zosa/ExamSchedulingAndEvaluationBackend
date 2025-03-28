﻿using Entrance.Shared.Models;

namespace Entrance.Tests.ServiceTests;
public class ExamResultShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformExaminationsMethods()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

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

        // Act & Assert: Verify applicant's exam result was recorded
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Examination);
        Assert.Equal(applicantModel.Data.Examination.Id, examId);
        Assert.Equal(80, applicantModel.Data.Examination.IntelligenceRawScore);
        Assert.Equal(33, applicantModel.Data.Examination.MathRawScore);
        Assert.Equal(35, applicantModel.Data.Examination.ReadingRawScore);
        Assert.Equal(34, applicantModel.Data.Examination.ScienceRawScore);

        // Arrange: Update examination result
        var updateModel = new ExaminationResultUpdate
        {
            Id = examId,
            IntelligenceRawScore = 99,
            MathRawScore = 35,
            ReadingRawScore = 25,
            ScienceRawScore = 30,
            UpdatedById = user.Data.Id
        };
        var updatedModel = await Connect.ExamResult.Update(updateModel);
        Assert.True(updatedModel.IsSuccessful);

        // Act & Assert: Verify applicant's exam result was updated
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Examination);
        Assert.Equal(applicantModel.Data.Examination.Id, examId);
        Assert.Equal(99, applicantModel.Data.Examination.IntelligenceRawScore);
        Assert.Equal(35, applicantModel.Data.Examination.MathRawScore);
        Assert.Equal(25, applicantModel.Data.Examination.ReadingRawScore);
        Assert.Equal(30, applicantModel.Data.Examination.ScienceRawScore);
    }
}

