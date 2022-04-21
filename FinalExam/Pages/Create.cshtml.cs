using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bowler.Models;

namespace Bowler.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Bowler.Models.BowlersDbContext _context;

        public CreateModel(Bowler.Models.BowlersDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TeamID"] = new SelectList(_context.Set<Teams>(), "TeamID", "TeamID");
            return Page();
        }

        [BindProperty]
        public Bowlers Bowlers { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bowlers.Add(Bowlers);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
