namespace RentBookApp.Data.Entities;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Publisher { get; set; }
    public DateOnly PublishedAt { get; set; }
    public string ISBN { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}



