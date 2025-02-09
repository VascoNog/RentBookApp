using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RentBookApp.Data;
using RentBookApp.Data.Entities;

namespace RentBookApp.Pages.AddRentals
{
    public class DeleteModel : PageModel
    {
        private readonly RentBookApp.Data.RentBookDbContext _context;

        public DeleteModel(RentBookApp.Data.RentBookDbContext context)
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

            var rental = await _context.Rentals.FirstOrDefaultAsync(m => m.Id == id);

            if (rental is not null)
            {
                Rental = rental;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                Rental = rental;
                _context.Rentals.Remove(Rental);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
