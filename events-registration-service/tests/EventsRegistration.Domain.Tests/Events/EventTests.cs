using EventsRegistration.Domain.Events;

namespace EventsRegistration.Domain.Tests.Events
{

    public class EventTests
    {
        [Fact]
        public void Create_WithValidData_ReturnsEventInDraftStatus()
        {
            var name = "Conferencia .NET 2026";
            var description = new string('a', 100);
            var venue = "Auditorio Central";
            var date = DateTime.UtcNow.AddDays(30);
            var capacity = 100;

            var evento = Event.Create(name, description, venue, date, capacity);

            Assert.Equal(EventStatus.Draft, evento.Status);
            Assert.NotEqual(Guid.Empty, evento.Id);
        }

        [Fact]
        public void Create_WithZeroCapacity_ThrowsException()
        {
            var name = "Conferencia .NET 2026";
            var description = new string('a', 100);
            var venue = "Auditorio Central";
            var date = DateTime.UtcNow.AddDays(30);
            var capacity = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => Event.Create(name, description, venue, date, capacity));
        }

        [Fact]
        public void Create_WithEmptyName_ThrowsException()
        {
            
            var description = new string('a', 100);
            var venue = "Auditorio Central";
            var date = DateTime.UtcNow.AddDays(30);
            var capacity = 100;

            Assert.Throws<ArgumentException>(() => Event.Create("", description, venue, date, capacity));
        }

        [Fact]
        public void Create_WithNameTooShort_ThrowsException()
        {
            var name = "Corto";
            var description = new string('a', 100);
            var venue = "Auditorio Central";
            var date = DateTime.UtcNow.AddDays(30);
            var capacity = 100;

            Assert.Throws<ArgumentException>(() => Event.Create(name, description, venue, date, capacity));
        }

        [Fact]
        public void Create_WithEmptyDescription_ThrowsException()
        {
            var name = "Conferencia .NET 2026";
            var venue = "Auditorio Central";
            var date = DateTime.UtcNow.AddDays(30);
            var capacity = 100;

            Assert.Throws<ArgumentException>(() => Event.Create(name, "", venue, date, capacity));
        }

        [Fact]
        public void Create_WithDescriptionTooShort_ThrowsException()
        {
            var name = "Conferencia .NET 2026";
            var description = new string('a', 50);
            var venue = "Auditorio Central";
            var date = DateTime.UtcNow.AddDays(30);
            var capacity = 100;

            Assert.Throws<ArgumentException>(() => Event.Create(name, description, venue, date, capacity));
        }

        [Fact]
        public void Create_WithEmptyVenue_ThrowsException()
        {
            var name = "Conferencia .NET 2026";
            var description = new string('a', 100);
            var date = DateTime.UtcNow.AddDays(30);
            var capacity = 100;

            Assert.Throws<ArgumentException>(() => Event.Create(name, description, "", date, capacity));
        }

        [Fact]
        public void Create_WithPastDate_ThrowsException()
        {
            var name = "Conferencia .NET 2026";
            var description = new string('a', 100);
            var venue = "Auditorio Central";
            var date = DateTime.UtcNow.AddDays(-1);
            var capacity = 100;

            Assert.Throws<ArgumentOutOfRangeException>(() => Event.Create(name, description, venue, date, capacity));
        }
        // Helper: evita repetir los mismos datos válidos en cada test
        private static Event CreateValidEvent(int capacity = 100)
        {
            return Event.Create("Conferencia .NET 2026", new string('a', 100),
                                "Auditorio Central", DateTime.UtcNow.AddDays(30), capacity);
        }

        [Fact]
        public void Create_WithValidData_SetsAvailableSeatsEqualToCapacity()
        {
            var evento = CreateValidEvent(capacity: 50);

            Assert.Equal(50, evento.AvailableSeats);
        }

        [Fact]
        public void Publish_WhenDraft_ChangesStatusToPublished()
        {
            var evento = CreateValidEvent();

            evento.Publish();

            Assert.Equal(EventStatus.Published, evento.Status);
        }

        [Fact]
        public void Publish_WhenAlreadyPublished_ThrowsException()
        {
            var evento = CreateValidEvent();
            evento.Publish();

            Assert.Throws<InvalidOperationException>(() => evento.Publish());
        }

        [Fact]
        public void ReserveSeat_WhenPublishedWithSeats_DecrementsAvailableSeats()
        {
            var evento = CreateValidEvent(capacity: 10);
            evento.Publish();

            evento.ReserveSeat();

            Assert.Equal(9, evento.AvailableSeats);
        }

        [Fact]
        public void ReserveSeat_WhenDraft_ThrowsException()
        {
            var evento = CreateValidEvent();

            Assert.Throws<InvalidOperationException>(() => evento.ReserveSeat());
        }

        [Fact]
        public void ReserveSeat_WhenNoSeatsLeft_ThrowsException()
        {
            var evento = CreateValidEvent(capacity: 1);
            evento.Publish();
            evento.ReserveSeat(); // se ocupa el único lugar

            Assert.Throws<InvalidOperationException>(() => evento.ReserveSeat());
        }



    }

}
