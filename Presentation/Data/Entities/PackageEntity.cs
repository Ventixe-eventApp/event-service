using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Data.Entities;

public class PackageEntity
{
    [Key]
    public int Id { get; set; }
    public string PackageName { get; set; } = null!;
    public string SeactionType { get; set; } = null!;
    public string? Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }

}

