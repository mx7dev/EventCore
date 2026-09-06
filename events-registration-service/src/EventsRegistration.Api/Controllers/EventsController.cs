using MediatR;
using Microsoft.AspNetCore.Mvc;
using EventsRegistration.Application.Events.CreateEvent;

namespace EventsRegistration.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return Ok(id);
    }
}
