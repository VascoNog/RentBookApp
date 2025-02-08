namespace RentBookApp.Pages.AllRentals;

[Authorize]
public class CreateModel : PageModel
{
    private readonly RentBookApp.Data.RentBookDbContext _context;

    public CreateModel(RentBookApp.Data.RentBookDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
    ViewData["BookId"] = new SelectList(_context.Books, "Id", "ISBN");
    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return Page();
    }

    [BindProperty]
    public Rental Rentals { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Rentals.Add(Rentals);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
