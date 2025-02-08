namespace RentBookApp.Pages.AllRentals;

public class IndexModel : PageModel
{
    private readonly RentBookApp.Data.RentBookDbContext _context;

    public IndexModel(RentBookApp.Data.RentBookDbContext context)
    {
        _context = context;
    }

    public IList<Rental> Rentals { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Rentals = await _context.Rentals
            .Include(r => r.Book)
            .Include(r => r.User).ToListAsync();
    }
}
