using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entrance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private ISender _sender = null;

    public ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
