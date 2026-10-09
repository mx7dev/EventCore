using EventsRegistration.Domain;
using MediatR;

namespace EventsRegistration.Application.Events.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = Event.Create(request.Name, request.Description, request.Venue, request.Date, request.Capacity);
            await _eventRepository.AddAsync(newEvent, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newEvent.Id;
        }
    }
}
