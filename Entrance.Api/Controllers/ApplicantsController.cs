using Entrance.Application.Feature.Management.Applicants.Commands;
using Entrance.Application.Feature.Management.Applicants.Queries;
using Entrance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entrance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicantsController : BaseController
{
    [HttpPost("transfer")]
    [Authorize]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        var response = await Sender.Send(new CreateTransferCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await Sender.Send(new DeleteApplicantCommand() { Id = id });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update-emergency-contact")]
    [Authorize]
    public async Task<IActionResult> UpdateEmergencyContact([FromBody] EmergencyContactUpdate update)
    {
        var response = await Sender.Send(new UpdateEmergencyContactCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update-gwa-status-track")]
    [Authorize]
    public async Task<IActionResult> UpdateGwaStatusTrack([FromBody] ApplicantUpdateGwaStatusTrack update)
    {
        var response = await Sender.Send(new UpdateGWAStatusTrackCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update-lrn")]
    [Authorize]
    public async Task<IActionResult> UpdateLRN([FromBody] ApplicantUpdateLrn update)
    {
        var response = await Sender.Send(new UpdateLrnCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update-personal-information")]
    [Authorize]
    public async Task<IActionResult> UpdatePersonalInformation([FromBody] PersonalInformationUpdate update)
    {
        var response = await Sender.Send(new UpdatePersonalInformationCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update-registered")]
    public async Task<IActionResult> UpdateRegistered([FromBody] ApplicantUpdateRegistered update)
    {
        var response = await Sender.Send(new UpdateRegisteredCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update-studentid")]
    [Authorize]
    public async Task<IActionResult> UpdateStudentId([FromBody] ApplicantUpdateStudentId update)
    {
        var response = await Sender.Send(new UpdateStudentIdCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update-transfer")]
    [Authorize]
    public async Task<IActionResult> UpdateTransfer([FromBody] ApplicantTransfer update)
    {
        var response = await Sender.Send(new UpdateTransferCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await Sender.Send(new GetApplicantQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("applicant-personal-information/{id}")]
    public async Task<IActionResult> GetApplicantPersonalInformation(int id)
    {
        var response = await Sender.Send(new GetApplicantPersonalInformationQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("applicant-student-information/{id}")]
    public async Task<IActionResult> GetApplicantStudentInformation(int id)
    {
        var response = await Sender.Send(new GetApplicantStudentInformationQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("applicant-application/{id}")]
    public async Task<IActionResult> GetApplicantApplication(int id)
    {
        var response = await Sender.Send(new GetApplicantApplicationQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("first-application/{id}")]
    [Authorize]
    public async Task<IActionResult> GetFirstApplication(int id)
    {
        var response = await Sender.Send(new GetFirstApplicationInfoQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("personal-information/{id}")]
    [Authorize]
    public async Task<IActionResult> GetPersonalInformation(int id)
    {
        var response = await Sender.Send(new GetPersonalInformationQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("lrn/{id}")]
    [Authorize]
    public async Task<IActionResult> GetLRN(int id)
    {
        var response = await Sender.Send(new GetLrnQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("list")]
    [Authorize]
    public async Task<IActionResult> List([FromQuery] DataGridQuery query, string access)
    {
        var response = await Sender.Send(new ListApplicantQuery { GridQuery = query, Access = access });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("list-schedule-applicants")]
    [Authorize]
    public async Task<IActionResult> ListScheduleApplicants([FromQuery] DataGridQuery query, string access, int schedId)
    {
        var response = await Sender.Send(new ListScheduleApplicantsQuery { GridQuery = query, Access = access, ScheduleId = schedId });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("list-inprogress")]
    [Authorize]
    public async Task<IActionResult> ListInProgress([FromQuery] DataGridQuery query, string access)
    {
        var response = await Sender.Send(new ListInProgressQuery { GridQuery = query, Access = access });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("passers")]
    [Authorize]
    public async Task<IActionResult> Passers([FromQuery] DataGridQuery query, string access)
    {
        var response = await Sender.Send(new ListPassersQuery { GridQuery = query, Access = access });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}

