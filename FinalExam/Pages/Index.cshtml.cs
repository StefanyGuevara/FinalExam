using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bowler.Models;

namespace Bowler.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Bowler.Models.BowlersDbContext _context;

        public IndexModel(Bowler.Models.BowlersDbContext context)
        {
            _context = context;
        }

        public IList<Bowlers> Bowlers { get; set; }

        public int teamID { get; set; }

        public async Task OnGetAsync(int id)
        {
            if (id == 0)
            {
                teamID = id;
                Bowlers = await _context.Bowlers
                    .Include(b => b.Team)
                    .ToListAsync();

                
            }
            else
            {
                teamID = id;
                Bowlers = await _context.Bowlers
                    .Include(b => b.Team)
                    .Where(b => b.TeamID == id)
                    .ToListAsync();
            }
        }


    }
}
