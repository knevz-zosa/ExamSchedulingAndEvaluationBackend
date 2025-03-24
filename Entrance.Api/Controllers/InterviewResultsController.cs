using Entrance.Application.Feature.Management.AssessmentsResults.Interviews.Commands;
using Entrance.Application.Feature.Management.AssessmentsResults.Interviews.Queries;
using Entrance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entrance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InterviewResultsController : BaseController
{
    [HttpPost("scores")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] InterviewResultRequest request)
    {
        var response = await Sender.Send(new CreateInterviewResultCommand(request));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var response = await Sender.Send(new GetInterviewResultQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] InterviewResultUpdate update)
    {
        var response = await Sender.Send(new UpdateInterviewResultCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("isactive")]
    [Authorize]
    public async Task<IActionResult> IsActive([FromBody] InterviewActiveUpdate update)
    {
        var response = await Sender.Send(new UpdateInterviewActiveCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("list")]
    [Authorize]
    public async Task<IActionResult> List([FromQuery] DataGridQuery query)
    {
        var response = await Sender.Send(new ListInterviewResultQuery { GridQuery = query });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
