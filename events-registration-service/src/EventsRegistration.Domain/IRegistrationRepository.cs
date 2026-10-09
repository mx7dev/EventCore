namespace EventsRegistration.Domain
{
    public interface IRegistrationRepository
    {
        Task AddAsync(Registration registration, CancellationToken cancellationToken);
    }
}
