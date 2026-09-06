using EventsRegistration.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventsRegistration.Infrastructure
{
    public class EventsRegistrationDbContext : DbContext
    {
        public EventsRegistrationDbContext(DbContextOptions<EventsRegistrationDbContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
    }
}
