using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bowler.Models;

namespace Bowler.Pages
{
    public class EditModel : PageModel
    {
        private readonly Bowler.Models.BowlersDbContext _context;

        public EditModel(Bowler.Models.BowlersDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bowlers Bowlers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bowlers = await _context.Bowlers
                .Include(b => b.Team).FirstOrDefaultAsync(m => m.BowlerID == id);

            if (Bowlers == null)
            {
                return NotFound();
            }
            ViewData["TeamName"] = new SelectList(_context.Set<Teams>(), "TeamName", "TeamName");
            ViewData["TeamID"] = new SelectList(_context.Set<Teams>(), "TeamID", "TeamID");
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

            _context.Attach(Bowlers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BowlersExists(Bowlers.BowlerID))
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
