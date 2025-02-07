namespace RentBookApp.Pages.AllBooks
{
    public class IndexModel : PageModel
    {
        private readonly BookRepository _bookRepository;
        public IndexModel(BookRepository bookRepository) => _bookRepository = bookRepository;
        public IList<Book> Book { get;set; }

        public async Task OnGetAsync()
        {
            Book = await _bookRepository.GetAllBooksAsync();
        }
    }
}
