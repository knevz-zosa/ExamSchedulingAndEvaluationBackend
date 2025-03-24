using Entrance.Application.Feature.Management.Schedules.Commands;
using Entrance.Application.Feature.Management.Schedules.Queries;
using Entrance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entrance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchedulesController : BaseController
{
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] ScheduleRequest request)
    {
        var response = await Sender.Send(new CreateScheduleCommand(request));

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
        var response = await Sender.Send(new DeleteScheduleCommand() { Id = id });

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
        var response = await Sender.Send(new GetScheduleQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("details/{id}")]
    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var response = await Sender.Send(new DetailsScheduleQuery(id));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("list")]
    public async Task<IActionResult> List([FromQuery] DataGridQuery query, string access)
    {
        var response = await Sender.Send(new ListScheduleQuery { GridQuery = query, Access = access });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("schoolyears")]
    [Authorize]
    public async Task<IActionResult> SchoolYears()
    {
        var response = await Sender.Send(new ListSchoolYearsQuery());

        if (response != null)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}

