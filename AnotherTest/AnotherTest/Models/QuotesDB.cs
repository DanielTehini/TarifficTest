using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherTest.Models
{
    using System.Data.Entity;

    public class QuotesDB : DbContext
    {
        public QuotesDB()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<UserQuotes> Quotes { get; set; }
    }
   
}
