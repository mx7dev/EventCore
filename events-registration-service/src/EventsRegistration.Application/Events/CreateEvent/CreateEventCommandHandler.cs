using EventsRegistration.Domain;
using MediatR;

namespace EventsRegistration.Application.Events.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = Event.Create(request.Name, request.Description, request.Venue, request.Date, request.Capacity);
            await _eventRepository.AddAsync(newEvent, cancellationToken);
            return newEvent.Id;
        }
    }

}
