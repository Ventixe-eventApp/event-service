using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class Event
{
    public string? ImagePath { get; set; }

    public string Name { get; set; } = null!;
 
    public string ArtistName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal Price { get; set; }
}
