namespace RentBookApp.Pages.AllBooks;

[Authorize]

public class EditModel : PageModel
{
    [BindProperty]
    public Book Book { get; set; }

    private readonly BookRepository _bookRepository;
    public EditModel(BookRepository bookRepository) => _bookRepository = bookRepository;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();
        var book =  await _bookRepository.GetBookAsync(id.Value);
        if (book == null)
            return NotFound();

        Book = book;
        ViewData["AuthorId"] = _bookRepository.GetAuthorSelectItems();
        //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _bookRepository.TrySaveEditionAsyncInDatabase(Book);
        return result.IsSuccess ? RedirectToPage("./Index") : Page();
    }


}
