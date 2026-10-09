using EventsRegistration.Application.Events.CreateEvent;
using EventsRegistration.Application.Events.GetEventById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var eventDto = await _mediator.Send(new GetEventByIdQuery(id), cancellationToken);
        return eventDto is null ? NotFound() : Ok(eventDto);
    }
}
