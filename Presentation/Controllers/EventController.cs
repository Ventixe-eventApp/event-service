using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(IEventService eventService) : ControllerBase
{

    public readonly IEventService _eventService = eventService;


    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventRequest req)
    {

        Console.WriteLine($"EventName: {req.EventName}");
        Console.WriteLine($"ArtistName: {req.ArtistName}");
        Console.WriteLine($"Location: {req.Location}");
        Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { sucess = false, errors });
        }

            var result = await _eventService.CreateEventAsync(req);

        if (result.Succeeded)
        {
            return Ok(new { message = "Event created successfully" });
        }
        else
        {
          
            return BadRequest(new { message = result.Error });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(string id)
    {
        var selectedEvent = await _eventService.GetEventByIdAsync(id);
        if (selectedEvent == null)
        {
            return NotFound(new { message = "Event not found" });
        }
        return Ok(selectedEvent);
    }
}
