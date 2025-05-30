using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class CreateEventRequest
{
    public string? ImagePath { get; set; }

    [Required(ErrorMessage = "You must enter a event name")]
    public string EventName { get; set; } = null!;
    [Required(ErrorMessage = "You must enter a artist")]
    public string ArtistName { get; set; } = null!;
    [Required(ErrorMessage = "You must enter a description")]
    public string Description { get; set; } = null!;
    [Required(ErrorMessage = "You must enter a location")]
    public string Location { get; set; } = null!;
    [Required(ErrorMessage = "You must enter a start date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "You must enter a start date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    public string? SeatmapImagePath { get; set; }


}


