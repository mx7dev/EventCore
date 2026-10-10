using EventsRegistration.Application.Events.CreateEvent;
using EventsRegistration.Application.Events.GetEventById;
using EventsRegistration.Application.Events.PublishEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using EventsRegistration.Api.Requests;
using EventsRegistration.Application.Registrations.RegisterAttendee;


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
    [HttpPost("{id:guid}/publish")]
    public async Task<IActionResult> Publish(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var found = await _mediator.Send(new PublishEventCommand(id), cancellationToken);
            return found ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            // Regla de negocio violada (ej. ya estaba publicado) → 409 Conflict
            return Conflict(ex.Message);
        }
    }
    [HttpPost("{id:guid}/registrations")]
    public async Task<IActionResult> Register(Guid id, RegisterAttendeeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new RegisterAttendeeCommand(id, request.AttendeeName, request.AttendeeEmail);
            var result = await _mediator.Send(command, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
        catch (ArgumentException ex)
        {
            // Datos inválidos (nombre vacío, email inválido) → 400
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            // Regla de negocio (evento no publicado, sin lugares) → 409
            return Conflict(ex.Message);
        }
    }


}
