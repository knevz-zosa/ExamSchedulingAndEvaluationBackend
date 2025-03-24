using Entrance.Application.Feature.Registration.Undergraduate.Commands;
using Entrance.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Entrance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UndergraduatesController : BaseController
{
    [HttpPost("application")]
    public async Task<IActionResult> Application([FromBody] ApplicantRequest request)
    {
        var response = await Sender.Send(new CreateApplicantCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("spouse")]
    public async Task<IActionResult> Spouse([FromBody] SpouseRequest request)
    {
        var response = await Sender.Send(new CreateSpouseCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("solo-parent")]
    public async Task<IActionResult> SoloParent([FromBody] SoloParentRequest request)
    {
        var response = await Sender.Send(new CreateSoloParentCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("first-application")]
    public async Task<IActionResult> FirstApplication([FromBody] FirstApplicationInfoRequest request)
    {
        var response = await Sender.Send(new CreateFirstApplicationInfoCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("personal-information")]
    public async Task<IActionResult> PersonalInformation([FromBody] PersonalInformationRequest request)
    {
        var response = await Sender.Send(new CreatePersonalInformationCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("academic-background")]
    public async Task<IActionResult> Academic([FromBody] AcademicBackgroundRequest request)
    {
        var response = await Sender.Send(new CreateAcademicBackgroundCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("parents-guardian-information")]
    public async Task<IActionResult> ParentsGuardian([FromBody] ParentGuardianInformationRequest request)
    {
        var response = await Sender.Send(new CreateParentGuardianInformationCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("family")]
    public async Task<IActionResult> Family([FromBody] FamilyRelationRequest request)
    {
        var response = await Sender.Send(new CreateFamilyRelationCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("counselor-consultation")]
    public async Task<IActionResult> Counselor([FromBody] CounselorConsultationRequest request)
    {
        var response = await Sender.Send(new CreateCounselorConsultationCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("psychiatrist-consultation")]
    public async Task<IActionResult> Psychiatrist([FromBody] PsychiatristConsultationRequest request)
    {
        var response = await Sender.Send(new CreatePsychiatristConsultationCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("psychologist-consultation")]
    public async Task<IActionResult> Psychologist([FromBody] PsychologistConsultationRequest request)
    {
        var response = await Sender.Send(new CreatePsychologistConsultationCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("physical-health")]
    public async Task<IActionResult> PhysicalHealth([FromBody] PhysicalHealthRequest request)
    {
        var response = await Sender.Send(new CreatePhysicalHealthCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("personality-profile")]
    public async Task<IActionResult> Personality([FromBody] PersonalityProfileRequest request)
    {
        var response = await Sender.Send(new CreatePersonalityProfileCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("emergency-contact")]
    public async Task<IActionResult> EmergencyContact([FromBody] EmergencyContactRequest request)
    {
        var response = await Sender.Send(new CreateEmergencyContactCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}

