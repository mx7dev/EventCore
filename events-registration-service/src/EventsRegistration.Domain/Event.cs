namespace EventsRegistration.Domain
{
    public class Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Venue { get; private set; }
        public DateTime Date { get; private set; }
        public int Capacity { get; private set; }
        public int AvailableSeats { get; private set; }
        public EventStatus Status { get; private set; }

        private Event(Guid id, string name, string description,
        string venue, DateTime date, int capacity, int availableSeats, EventStatus status)
        {
            Id = id;
            Name = name;
            Description = description;
            Venue = venue;
            Date = date;
            Capacity = capacity;
            AvailableSeats = availableSeats;
            Status = status;
        }

        public static Event Create(string name, string description,
                                    string venue, DateTime date, int capacity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            if (name.Length < 10)
                throw new ArgumentException("Name must be at least 10 characters");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required");

            if (description.Length < 100)
                throw new ArgumentException("Description must be at least 100 characters");

            if (string.IsNullOrWhiteSpace(venue))
                throw new ArgumentException("Venue is required");

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

            ArgumentOutOfRangeException.ThrowIfLessThan(date, DateTime.UtcNow);

            // Al nacer, todos los lugares están disponibles
            return new Event(Guid.NewGuid(), name, description, venue, date, capacity, capacity, EventStatus.Draft);
        }

        public void Publish()
        {
            if (Status != EventStatus.Draft)
                throw new InvalidOperationException("Only draft events can be published");

            Status = EventStatus.Published;
        }

        public void ReserveSeat()
        {
            if (Status != EventStatus.Published)
                throw new InvalidOperationException("Registrations are only allowed for published events");

            if (AvailableSeats <= 0)
                throw new InvalidOperationException("No seats available");

            AvailableSeats--;
        }
    }
}
