using System.Security.Claims;

namespace RentBookApp.Pages.AddRentals;

[Authorize]

public class CreateModel : PageModel
{
    [BindProperty]
    public Rental Rental { get; set; }


    private readonly BookRepository _bookRepository;
    public CreateModel(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IActionResult OnGet()
    {
        var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ViewData["BookId"] = _bookRepository.GetAvailableBooksForRentSelectItems(_userId);

        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

       
        await _bookRepository.ChangeCurrentBookAvailabilityToFalse(Rental.BookId);
        Rental.UserId = _bookRepository.GetOwnerId(Rental.BookId); // Associar o Owner correspondente
        await _bookRepository.AddRental(Rental);
        await _bookRepository.SaveChangesAsyncInDatabase();

        return RedirectToPage("./Index");
    }
}
