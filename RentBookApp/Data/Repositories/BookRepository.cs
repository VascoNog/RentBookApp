using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.Configuration;
using RentBookApp.Models;
using System.Security.Claims;

namespace RentBookApp.Data.Repositories;

public class BookRepository
{
    private readonly RentBookDbContext _ctx;

    public BookRepository(RentBookDbContext ctx) => _ctx = ctx;

    public async Task AddBook(Book book) => await _ctx.Books.AddAsync(book);

    public async Task AddRental(Rental rental) => await _ctx.Rentals.AddAsync(rental);

    public async Task<int> SaveChangesAsyncInDatabase() => await _ctx.SaveChangesAsync();

    public SelectList GetAuthorSelectItems() => new SelectList(_ctx.Set<Author>().ToList().DistinctBy(a => a.AuthorName), "Id", "AuthorName");


    // GET AVAILABLE BOOKS
    public SelectList GetAvailableBookSelectItems() => new SelectList(_ctx.Set<Book>().ToList(), "Id", "Title");
    public SelectList GetUserSelectItems() => new SelectList(_ctx.Users, "Id", "Email");


    public SelectList GetAvailableBooksForRentSelectItems(string userId) => new SelectList(GetAvailableBooksForRent(userId), "Id", "BookAndOwner");
    public SelectList GetAvailableBookOwnersForRentSelectItems(string userId) => new SelectList(GetAvailableBooksForRent(userId), "OwnerId", "OwnerEmail");

    public IList<BookModel> GetAvailableBooksForRent(string userId)
    {
        return (from b in _ctx.Books
                join u in _ctx.Users on b.UserId equals u.Id
                where b.IsAvailable == true && b.UserId != userId
                select new BookModel
                {
                    Id = b.Id,
                    BookAndOwner = $"{b.Title} - {b.User.Email}",
                    OwnerEmail = u.Email,
                    //OwnerId = b.UserId,
                })
                .ToList();
    }


    public async Task<IList<Book>> GetAllBooksAsync() => await _ctx.Books
         .Include(b => b.Author)
         .Include(b => b.User).ToListAsync();

    public async Task<Book> GetBookAsync(int id) => await _ctx.Books
        .Include(b => b.Author)
        .Include(b => b.User)
        .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<Book> GetBookByIdAsync(int id) => await _ctx.Books.FirstOrDefaultAsync(b => b.Id == id);

    public async Task RemoveBookAsync(Book book) => _ctx.Books.Remove(book);

    public void AttachBookStateModified(Book Book) => _ctx.Attach(Book).State = EntityState.Modified;

    public bool BookExists(int id) => _ctx.Books.Any(e => e.Id == id);

    public async Task<Result> TrySaveEditionAsyncInDatabase(Book book)
    {
        try
        {
            _ctx.Attach(book).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Result.Ok();
        }
        catch
        {
            return Result.Fail("Error Updating Book");
        }
    }

    public async Task RemoveBookAsyncAndSaveChanges(int id)
    {
        var book = await _ctx.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book != null)
        {
            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
        }
    }


    public async Task<IList<Rental>> GetAllRentalsAsync() => await _ctx.Rentals
        .Include(r => r.Book)
        .Include(r => r.User).ToListAsync();

    //Rental = await _context.Rentals
    //.Include(r => r.Book)
    //.Include(r => r.User).ToListAsync();




    public async Task ChangeCurrentBookAvailabilityToFalse(int currentBookId)
    {
        _ctx.Books.FirstOrDefault(b => b.Id == currentBookId).IsAvailable = false;
    }

    //ChangeCurrentBookAvailability(Rental.BookId);


    public string GetOwnerId(int bookId) => _ctx.Books.FirstOrDefault(b => b.Id == bookId).UserId;

}