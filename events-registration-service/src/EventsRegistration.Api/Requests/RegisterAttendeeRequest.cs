namespace EventsRegistration.Api.Requests
{
    // Lo que manda el cliente en el body. El EventId viene en la URL, no acá.
    public record RegisterAttendeeRequest(string AttendeeName, string AttendeeEmail);
}
