using EventsRegistration.Domain;
using Microsoft.EntityFrameworkCore;

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
        }

        public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
