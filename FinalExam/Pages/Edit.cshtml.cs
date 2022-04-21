using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quote.Models;

namespace Quote.Pages
{
    public class EditModel : PageModel
    {
        private readonly Quote.Models.QuotesDbContext _context;

        public EditModel(Quote.Models.QuotesDbContext context)
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

            quote = await _context.Bowlers
                .Include(b => b.Team).FirstOrDefaultAsync(m => m.QuoteID == id);

            if (quote == null)
            {
                return NotFound();
            }
            ViewData["Author"] = new SelectList(_context.Set<Quote>(), "Author", "Author");
            ViewData["QuoteID"] = new SelectList(_context.Set<Quote>(), "QuoteID", "QuoteID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BowlersExists(quote.QuoteID))
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

        private bool BowlersExists(int id)
        {
            return _context.Bowlers.Any(e => e.BowlerID == id);
        }
    }
}
