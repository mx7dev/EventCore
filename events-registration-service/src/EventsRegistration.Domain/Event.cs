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
        public EventStatus Status { get; private set; }

        private Event(Guid id, string name, string description,
        string venue, DateTime date, int capacity, EventStatus status)
        {
            Id = id;
            Name = name;
            Description = description;
            Venue = venue;
            Date = date;
            Capacity = capacity;
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

            return new Event(Guid.NewGuid(), name, description, venue, date, capacity, EventStatus.Draft);
        }
    }
}