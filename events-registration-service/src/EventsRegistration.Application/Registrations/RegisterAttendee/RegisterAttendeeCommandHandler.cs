using EventsRegistration.Domain;
using MediatR;

namespace EventsRegistration.Application.Registrations.RegisterAttendee
{
    public class RegisterAttendeeCommandHandler : IRequestHandler<RegisterAttendeeCommand, RegisterAttendeeResult?>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterAttendeeCommandHandler(IEventRepository eventRepository,
            IRegistrationRepository registrationRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterAttendeeResult?> Handle(RegisterAttendeeCommand request, CancellationToken cancellationToken)
        {
            // 1. Cargar el evento
            var existingEvent = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);
            if (existingEvent is null)
                return null;

            // 2. El evento valida (publicado + hay lugar) y descuenta
            existingEvent.ReserveSeat();

            // 3. Nace el registro con su TicketToken
            var registration = Registration.Create(existingEvent.Id, request.AttendeeName, request.AttendeeEmail);
            await _registrationRepository.AddAsync(registration, cancellationToken);

            // 4. Una sola transacción: se guardan los dos, o ninguno
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterAttendeeResult(registration.Id, registration.TicketToken);
        }
    }
}
