namespace AidManager.Collaborate.Domain.Entities;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; } // Changed string to DateTime for better type safety
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty; // Could be an enum or a simple string
    public int ProjectId { get; set; } // Assuming ProjectId links to a Project entity (external or to be defined)
}
