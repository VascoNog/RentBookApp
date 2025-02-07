namespace RentBookApp.Data.Entities;

public class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public string Nationality { get; set; }
    public DateOnly BornAt { get; set; }
}
