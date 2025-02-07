namespace RentBookApp.Data.Repositories;

public class BookRepository
{
    private readonly RentBookDbContext _ctx;

    public BookRepository(RentBookDbContext ctx) => _ctx = ctx;

    public async Task AddBook(Book book) => await _ctx.Books.AddAsync(book);

    public async Task<int> SaveChangesAsyncInDatabase() => await _ctx.SaveChangesAsync();

    public SelectList GetAuthorSelectItems() => new SelectList(_ctx.Set<Author>().ToList().DistinctBy(a => a.AuthorName), "Id", "AuthorName");

    public SelectList GetUserSelectItems() => new SelectList(_ctx.Users, "Id", "Email");

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
}