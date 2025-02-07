namespace RentBookApp.Pages.AllBooks
{
    public class DeleteModel : PageModel
    {
        private readonly BookRepository _bookRepository;
        public DeleteModel(BookRepository bookRepository) => _bookRepository = bookRepository;

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _bookRepository.GetBookAsync(id.Value);
            return Book is null ? NotFound() : Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(!_bookRepository.BookExists(id.Value))
                return NotFound();

            await _bookRepository.RemoveBookAsyncAndSaveChanges(id.Value);
            return RedirectToPage("./Index");
        }
    }
}
