using System;
using System.ComponentModel.DataAnnotations;

namespace Quote.Models
{
   
    public class Quotes
    {
        [Key]
        [Required]
        public int QuotesID { get; set; }

        public string Author { get; set; }

        public int Date { get; set; }

        public string Subject { get; set; }

        public string Citation { get; set; }

    }
}
