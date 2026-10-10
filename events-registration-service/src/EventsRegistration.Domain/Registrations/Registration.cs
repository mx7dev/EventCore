namespace EventsRegistration.Domain.Registrations
{
    public class Registration
    {
        public Guid Id { get; private set; }
        public Guid EventId { get; private set; }
        public string AttendeeName { get; private set; }
        public string AttendeeEmail { get; private set; }
        public Guid TicketToken { get; private set; }
        public DateTime RegisteredAt { get; private set; }

        private Registration(Guid id, Guid eventId, string attendeeName,
            string attendeeEmail, Guid ticketToken, DateTime registeredAt)
        {
            Id = id;
            EventId = eventId;
            AttendeeName = attendeeName;
            AttendeeEmail = attendeeEmail;
            TicketToken = ticketToken;
            RegisteredAt = registeredAt;
        }

        public static Registration Create(Guid eventId, string attendeeName, string attendeeEmail)
        {
            if (eventId == Guid.Empty)
                throw new ArgumentException("EventId is required");

            if (string.IsNullOrWhiteSpace(attendeeName))
                throw new ArgumentException("Attendee name is required");

            if (string.IsNullOrWhiteSpace(attendeeEmail) || !attendeeEmail.Contains('@'))
                throw new ArgumentException("A valid attendee email is required");

            return new Registration(Guid.NewGuid(), eventId, attendeeName, attendeeEmail,
                                    Guid.NewGuid(), DateTime.UtcNow);
        }
    }
}
