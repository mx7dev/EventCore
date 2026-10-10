using MediatR;

namespace EventsRegistration.Application.Registrations.RegisterAttendee
{
    // Devuelve null si el evento no existe
    public record RegisterAttendeeCommand(Guid EventId, string AttendeeName, string AttendeeEmail)
        : IRequest<RegisterAttendeeResult?>;

    public record RegisterAttendeeResult(Guid RegistrationId, Guid TicketToken);
}
