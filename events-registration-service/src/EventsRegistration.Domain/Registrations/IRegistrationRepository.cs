namespace EventsRegistration.Domain.Registrations
{
    public interface IRegistrationRepository
    {
        Task AddAsync(Registration registration, CancellationToken cancellationToken);
    }
}
