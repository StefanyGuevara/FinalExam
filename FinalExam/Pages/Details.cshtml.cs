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
    public class DetailsModel : PageModel
    {
        private readonly Quote.Models.QuotesDbContext _context;

        public DetailsModel(Quote.Models.QuotesDbContext context)
        {
            _context = context;
        }

        public Quote Quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quote = await _context.Quote
                .Include(b => b.Team).FirstOrDefaultAsync(m => m.QuoteID == id);

            if (Quote == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
