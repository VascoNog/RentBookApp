using NuGet.Packaging.Signing;
using System.Security.Claims;

namespace RentBookApp.Pages.AllBooks;

[Authorize]
public class CreateModel : PageModel
{
    [BindProperty]
    public Book Book { get; set; }

    private readonly BookRepository _bookRepository;
    public CreateModel(BookRepository bookRepository) => _bookRepository = bookRepository;

    public IActionResult OnGet()
    {
        ViewData["AuthorId"] = _bookRepository.GetAuthorSelectItems().Distinct();
        //ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Book.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Book.IsAvailable = true;

        await _bookRepository.AddBook(Book);

        await _bookRepository.SaveChangesAsyncInDatabase();

        return RedirectToPage("./Index");
    }
}