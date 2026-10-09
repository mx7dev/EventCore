using MediatR;

namespace EventsRegistration.Application.Events.GetEventById
{
    public record GetEventByIdQuery(Guid Id) : IRequest<EventDto?>;
}
