using EventsRegistration.Domain;
using MediatR;

namespace EventsRegistration.Application.Events.GetEventById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto?>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByIdQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingEvent is null)
                return null;

            return new EventDto(
                existingEvent.Id,
                existingEvent.Name,
                existingEvent.Description,
                existingEvent.Venue,
                existingEvent.Date,
                existingEvent.Capacity,
                existingEvent.Status.ToString());
        }
    }
}
