namespace EventsRegistration.Application.Events.GetEventById
{
    public record EventDto(
     Guid Id,
     string Name,
     string Description,
     string Venue,
     DateTime Date,
     int Capacity,
     int AvailableSeats,
     string Status);
}
