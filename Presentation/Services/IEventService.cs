using Presentation.Models;

namespace Presentation.Services;

public interface IEventService
{
    Task<EventResult> CreateEventAsync(CreateEventRequest form);

    Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync();

    Task<EventResult<Event?>> GetEventByIdAsync(string id);
}
