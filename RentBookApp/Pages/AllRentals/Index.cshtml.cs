namespace RentBookApp.Pages.AddRentals;

public class IndexModel : PageModel
{
    private readonly BookRepository _bookRepository;
    public IndexModel(BookRepository bookRepository) => _bookRepository = bookRepository;


    public IList<Rental> Rentals { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Rentals = await _bookRepository.GetAllRentalsAsync();
    }
}
