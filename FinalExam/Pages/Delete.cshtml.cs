using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quote.Models;

namespace Quote.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Quote.Models.QuotesDbContext _context;

        public DeleteModel(Quote.Models.QuotesDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quote quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            quote = await _context.quote
                .Include(b => b.Team).FirstOrDefaultAsync(m => m.QuoteID == id);

            if (Quote == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            quote = await _context.Bowlers.FindAsync(id);

            if (quote != null)
            {
                _context.Bowlers.Remove(quote);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
