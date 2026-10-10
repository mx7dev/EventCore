using EventsRegistration.Domain.Registrations;
using EventsRegistration.Infrastructure.Persistence;

namespace EventsRegistration.Infrastructure.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly EventsRegistrationDbContext _context;

        public RegistrationRepository(EventsRegistrationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Registration registration, CancellationToken cancellationToken)
        {
            await _context.Registrations.AddAsync(registration, cancellationToken);
        }
    }
}
