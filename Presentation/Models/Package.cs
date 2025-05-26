namespace Presentation.Models;

public class Package
{
    public string Id { get; set; } = null!;
    public string PackageName { get; set; } = null!;
    public string SeactionType { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }


}
