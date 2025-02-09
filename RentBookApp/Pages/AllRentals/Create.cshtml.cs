using Microsoft.Identity.Client;
using System.Security.Claims;

namespace RentBookApp.Pages.AddRentals;

[Authorize]

public class CreateModel : PageModel
{
    [BindProperty]
    public Rental Rental { get; set; }

    private string _userId;

    private readonly BookRepository _bookRepository;
    public CreateModel(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IActionResult OnGet()
    {
        _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //ViewData["Books"] = _bookRepository.GetAvailableBooksForRent();
        ViewData["BookId"] = _bookRepository.GetAvailableBooksForRentSelectItems(_userId);
        //ViewData["UserId"] = _bookRepository.GetUserSelectItems();

        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _bookRepository.ChangeCurrentBookAvailabilityToFalse(Rental.BookId);
        await _bookRepository.AddRental(Rental);
        await _bookRepository.SaveChangesAsyncInDatabase();

        return RedirectToPage("./Index");
    }
}
