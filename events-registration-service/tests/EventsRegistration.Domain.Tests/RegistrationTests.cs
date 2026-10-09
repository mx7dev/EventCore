namespace EventsRegistration.Domain.Tests
{
    public class RegistrationTests
    {
        [Fact]
        public void Create_WithValidData_GeneratesIdAndTicketToken()
        {
            var registration = Registration.Create(Guid.NewGuid(), "Maximo Carrion", "maximo@test.com");

            Assert.NotEqual(Guid.Empty, registration.Id);
            Assert.NotEqual(Guid.Empty, registration.TicketToken);
            Assert.NotEqual(registration.Id, registration.TicketToken);
        }

        [Fact]
        public void Create_WithEmptyEventId_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                Registration.Create(Guid.Empty, "Maximo Carrion", "maximo@test.com"));
        }

        [Fact]
        public void Create_WithEmptyName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                Registration.Create(Guid.NewGuid(), "  ", "maximo@test.com"));
        }

        [Fact]
        public void Create_WithInvalidEmail_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                Registration.Create(Guid.NewGuid(), "Maximo Carrion", "correo-invalido"));
        }
    }
}
