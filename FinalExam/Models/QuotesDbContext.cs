using System;
using Microsoft.EntityFrameworkCore;

namespace Bowler.Models
{
    public class QuotesDbContext: DbContext
    {
        public QuotesDbContext(DbContextOptions<QuotesDbContext> options) : base(options)
        {

        }

        public DbSet<Quote> Bowlers { get; set; }
    }
}
