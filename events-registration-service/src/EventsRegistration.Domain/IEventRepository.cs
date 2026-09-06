namespace EventsRegistration.Domain
{
    public interface IEventRepository
    {
        Task AddAsync(Event @event, CancellationToken cancellationToken);
    }
}
