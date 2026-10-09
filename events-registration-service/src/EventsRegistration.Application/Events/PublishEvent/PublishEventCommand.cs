using MediatR;

namespace EventsRegistration.Application.Events.PublishEvent
{
    // Devuelve false si el evento no existe
    public record PublishEventCommand(Guid EventId) : IRequest<bool>;
}
