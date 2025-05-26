using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Presentation.Data.Contexts;
using Presentation.Data.Entities;
using Presentation.Data.Repositories;
using Presentation.Models;

namespace Presentation.Services;

public interface IEventService
{
    Task<EventResult> CreateEventAsync(CreateEventRequest form);

    Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync();

    Task<EventResult<Event?>> GetEventByIdAsync(string id);
}

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;


    public async Task<EventResult> CreateEventAsync(CreateEventRequest req)
    {
        try
        {

            var entity = new EventEntity
            {  
                ImagePath = req.ImagePath,
                EventName = req.EventName,
                ArtistName = req.ArtistName,
                Description = req.Description,
                Location = req.Location,
                StartDate = req.StartDate,
                EndDate = req.EndDate,

            };

            var result = await _eventRepository.CreateAsync(entity);
            return result.Succeeded
                ? new EventResult { Succeeded = true }
                : new EventResult { Succeeded = false, Error = result.Error };

        }
        catch (Exception ex)
        {
            return new EventResult
            {
                Succeeded = false,
                Error = ex.Message
            };
        }

    }

    public async Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync()
    {
        var result = await _eventRepository.GetAll();
        var events = result.Result?.Select(x => new Event
        {
            Id = x.Id,
            ImagePath = x.ImagePath,
            EventName = x.EventName,
            ArtistName = x.ArtistName,
            Description = x.Description,
            Location = x.Location,
            StartDate = x.StartDate,
            EndDate = x.EndDate
        });

        return new EventResult<IEnumerable<Event>>
        {
            Succeeded = result.Succeeded,
            Result = events
        };
    }

    public async Task<EventResult<Event?>> GetEventByIdAsync(string id)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == id);
        if (result.Succeeded && result.Result != null)
        {
            var selectedEvent = new Event
            {
                Id = result.Result.Id,
                ImagePath = result.Result.ImagePath,
                EventName = result.Result.EventName,
                ArtistName = result.Result.ArtistName,
                Description = result.Result.Description,
                Location = result.Result.Location,
                StartDate = result.Result.StartDate,
                EndDate = result.Result.EndDate,
                Packages = result.Result.Packages.Select(p => new Package
                {
                    Id = p.Id,
                    PackageName = p.Package.PackageName,
                    Price = p.Package.Price,
                    Description = p.Package.Description,
                    AvailableQuantity = p.Package.AvailableQuantity,
                }).ToList()
            };
            return new EventResult<Event?>
            {
                Succeeded = true,
                Result = selectedEvent
            };
        }
        else
        {
            return new EventResult<Event?>
            {
                Succeeded = false,
                Error = result.Error
            };
        }

    }

}
