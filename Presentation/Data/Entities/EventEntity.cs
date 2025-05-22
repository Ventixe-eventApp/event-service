using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? ImagePath { get; set; }

    [Required]
    public string EventName { get; set; } = null!;

    [Required]
    public string ArtistName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string Location { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime Created { get; set; } = DateTime.Now;

    public ICollection<EventPackageEntity> Packages { get; set; } = [];


}

