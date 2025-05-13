using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Services;

public class EventService(DataContext context, IFileService fileService)
{
    private readonly DataContext _context = context;
    private readonly IFileService _fileService = fileService;

    public async Task<(bool Success, string ErrorMessage)> CreateEventAsync (EventRegistrationForm form)
    {
        ArgumentNullException.ThrowIfNull(form);

      try
      {

            if (form.EventImage != null)
            {
                try
                {
                    var filePath = await _fileService.SaveFileAsync(form.EventImage);
                    form.ImagePath = filePath;
                }
                catch (Exception ex)
                {
                    return (false, $"Error saving image: {ex.Message}");

                }
            }


            var entity = new EventEntity
            {
                ImagePath = form.ImagePath,
                EventName = form.EventName,
                ArtistName = form.ArtistName,
                Description = form.Description,
                Location = form.Location,
                StartDate = form.StartDate,
                EndDate = form.EndDate,
                Price = form.Price
            };

            _context.Events.Add(entity);
            await _context.SaveChangesAsync();

            return (true, string.Empty);
           
        }
        catch (Exception ex)
        {
            return(false, $"Error saving image: {ex.Message}");
        }

    }

    public async Task<IEnumerable<EventEntity>> GetAllEventsAsync()
    {
        var events = await _context.Events.ToListAsync();
       return events;
    }

    public async Task<EventEntity?> GetEventByIdAsync(string id)
    {
        var eventEntity = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
        return eventEntity;
    }



}
