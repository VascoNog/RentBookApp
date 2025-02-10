namespace RentBookApp.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Publisher { get; set; }
    public DateOnly PublishedAt { get; set; }
    public string ISBN { get; set; }
    public string OwnerId { get; set; }
    public string OwnerEmail { get; set; }
    public string RenterEmail { get; set; }
    public int AuthorId { get; set; }
    public bool IsAvailable { get; set; }
    public string BookAndOwner { get; set; }

}