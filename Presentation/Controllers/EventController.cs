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


    [HttpPost("create")]
    public async Task<IActionResult> CreateEvent([FromForm] EventRegistrationForm form)
    {
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


            var result = await _eventService.CreateEventAsync(form);

        if (result.Success)
        {
            return Ok(new { message = "Event created successfully" });
        }
        else
        {
            return BadRequest(new { message = result.ErrorMessage });
        }
    }
}
