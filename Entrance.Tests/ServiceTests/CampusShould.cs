using Entrance.Domain.Entities;
using Entrance.Shared.Models;

namespace Entrance.Tests.ServiceTests;
public class CampusShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformCampusesMethods()
    {
        // Arrange: Authenticate first

        var userData = await LoginDefault();

        // Arrange: Create Campus
        var campusRequest = new CampusRequest
        {
            Name = "Campus123",
            HasDepartment = false,
            Address = "Current Address",
            CreatedById = userData.Data.Id
        };

        var campusResult = await Connect.Campus.Create(campusRequest);
        Assert.True(campusResult.IsSuccessful);
        var campusId = campusResult.Data;

        // Act & Assert: Verify campus was created
        var campusModel = await Connect.Campus.Get(campusId);
        Assert.NotNull(campusModel.Data);
        Assert.Equal("Campus123", campusModel.Data.Name);

        //DETAILS
        var campusDetails = await Connect.Campus.Details(campusId);
        Assert.NotNull(campusDetails.Data);
        Assert.Equal("Campus123", campusDetails.Data.Name);
        Assert.Equal("Current Address", campusDetails.Data.Address);
        Assert.False(campusDetails.Data.HasDepartment);

        // LIST
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 10,
            SortField = nameof(Campus.Name),
            SortDir = DataGridQuerySortDirection.Ascending
        };

        var campusModels = await Connect.Campus.List(listQuery);
        Assert.NotNull(campusModels);
        Assert.True(campusModels.IsSuccessful);
        Assert.True(campusModels.Data.List.Any());
        Assert.Contains(campusModels.Data.List, x => x.Id == campusModel.Data.Id);

        // Validation: Try creating another campus with the same name, expect failure
        var duplicateCampusRequest = new CampusRequest
        {
            Name = "Campus123",  // Same name as the first campus
            HasDepartment = true,
            Address = "Duplicate Address",
            CreatedById = userData.Data.Id
        };

        // Act: Attempt to create campus
        var invalidCampusResponse = await Connect.Campus.Create(duplicateCampusRequest);

        Assert.False(invalidCampusResponse.IsSuccessful);
        Assert.Equal($"Name already exists.", invalidCampusResponse.Messages.FirstOrDefault());

        // Arrange: Create Different Campus
        var newCampusRequest = new CampusRequest
        {
            Name = "Campus1234",
            HasDepartment = true,
            Address = "New Address",
            CreatedById = userData.Data.Id
        };

        var newCampusResult = await Connect.Campus.Create(newCampusRequest);
        Assert.True(newCampusResult.IsSuccessful);
        var newCampusId = newCampusResult.Data;

        // Act & Assert: Verify campus was created
        var newCampusModel = await Connect.Campus.Get(newCampusId);
        Assert.NotNull(newCampusModel.Data);
        Assert.Equal("Campus1234", newCampusModel.Data.Name);


        //Validation: Try updating another campus to the name "Campus1234", expect failure
        var updateModel = new CampusUpdate
        {
            Id = campusId,
            Name = "Campus1234",
            Address = "Catbalogan City",
            HasDepartment = true,
            UpdatedById = userData.Data.Id
        };

        // Act: Attempt to create campus
        var invalidCampusUpdate = await Connect.Campus.Update(updateModel);

        Assert.False(invalidCampusUpdate.IsSuccessful);
        Assert.Equal($"Name already exists.", invalidCampusUpdate.Messages.FirstOrDefault());

        // Arrange: Update campus
        updateModel = new CampusUpdate
        {
            Id = campusId,
            Name = "Campus12345",
            Address = "Catbalogan City",
            HasDepartment = true,
            UpdatedById = userData.Data.Id
        };

        var updatedModel = await Connect.Campus.Update(updateModel);
        Assert.True(updatedModel.IsSuccessful);

        // Act & Assert: Verify campus was updated
        var updatedModelResult = await Connect.Campus.Get(campusId);
        Assert.NotNull(updatedModelResult.Data);
        Assert.Equal("Campus12345", updatedModelResult.Data.Name);

        // Arrange: Delete campus
        var deleteModel = await Connect.Campus.Delete(campusId);
        Assert.True(deleteModel.IsSuccessful);

        // Act & Assert: Verify campus was deleted
        campusModels = await Connect.Campus.List(listQuery);
        Assert.NotNull(campusModels);
        Assert.DoesNotContain(campusModels.Data.List, x => x.Id == campusModel.Data.Id);
    }
}


