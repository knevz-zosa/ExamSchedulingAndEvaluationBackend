using Entrance.Application.Feature.Management.Courses.Commands;
using Entrance.Application.Feature.Management.Courses.Queries;
using Entrance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entrance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : BaseController
{
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CourseRequest request)
    {
        var response = await Sender.Send(new CreateCourseCommand(request));

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
        var response = await Sender.Send(new DeleteCourseCommand() { Id = id });

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
        var response = await Sender.Send(new GetCourseQuery(id));

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
        var response = await Sender.Send(new DetailsCourseQuery(id));

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
        var response = await Sender.Send(new ListCourseQuery { GridQuery = query, Access = access });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateAccess([FromBody] CourseUpdate update)
    {
        var response = await Sender.Send(new UpdateCourseCommand(update));

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}