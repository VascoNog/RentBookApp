

namespace RentBookApp.Data.Entities;

public class Rental
{
    public int Id { get; set; }
    public DateTime RentedAt { get; set; }
    public DateTime ReturnedAt { get; set; }

    // FK BookId
    public int BookId { get; set; }
    public Book Book { get; set; }

    // FK UserId
    public string UserId { get; set; }
    public  IdentityUser User { get; set; }
}
