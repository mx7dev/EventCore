using EventsRegistration.Domain.Common;
using EventsRegistration.Domain.Events;
using MediatR;

namespace EventsRegistration.Application.Events.PublishEvent
{
    public class PublishEventCommandHandler : IRequestHandler<PublishEventCommand, bool>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublishEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(PublishEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);

            if (existingEvent is null)
                return false;

            existingEvent.Publish();  // el dominio valida que esté en Draft
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
