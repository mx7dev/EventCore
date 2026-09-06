using EventsRegistration.Domain;

namespace EventsRegistration.Infrastructure
{
    public class EventRepository : IEventRepository
    {
        private readonly EventsRegistrationDbContext _context;

        public EventRepository(EventsRegistrationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Event @event, CancellationToken cancellationToken)
        {
            await _context.Events.AddAsync(@event, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
