using MediatR;

namespace EventsRegistration.Application.Events.CreateEvent
{
    public record CreateEventCommand(
    string Name,
    string Description,
    DateTime Date,
    string Venue,
    int Capacity) : IRequest<Guid>;

}
