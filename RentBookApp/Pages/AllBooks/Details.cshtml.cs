namespace RentBookApp.Pages.AllBooks;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly BookRepository _bookRepository;

    public DetailsModel(BookRepository bookRepository) => _bookRepository = bookRepository;
    
    public Book Book { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id == null)
            return NotFound();
        
        Book = await _bookRepository.GetBookAsync(id);
        return Book == null ? NotFound() : Page();
    }
}
