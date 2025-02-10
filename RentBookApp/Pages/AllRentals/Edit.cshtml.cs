namespace RentBookApp.Pages.AddRentals;

[Authorize]
public class EditModel : PageModel
{
    private readonly RentBookApp.Data.RentBookDbContext _context;

    public EditModel(RentBookApp.Data.RentBookDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Rental Rental { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rental =  await _context.Rentals.FirstOrDefaultAsync(m => m.Id == id);
        if (rental == null)
        {
            return NotFound();
        }
        Rental = rental;
        ViewData["BookId"] = new SelectList(_context.Books, "Id", "ISBN");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Rental).State = EntityState.Modified;

        try
        {
            if(Rental.ReturnedAt != null)
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == Rental.BookId);

                if (book != null)
                {
                    book.IsAvailable = true;
                }

                await _context.SaveChangesAsync();
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RentalExists(Rental.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool RentalExists(int id)
    {
        return _context.Rentals.Any(e => e.Id == id);
    }
}
