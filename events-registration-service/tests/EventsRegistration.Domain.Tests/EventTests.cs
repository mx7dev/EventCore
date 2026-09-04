namespace EventsRegistration.Domain.Tests
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


    }

}
