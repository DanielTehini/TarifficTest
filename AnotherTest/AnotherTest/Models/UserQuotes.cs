using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherTest.Models
{
    public class UserQuotes
    {
        public int Id { get; set; }
        public string Author { get; set; }

        public string Quote { get; set; }

        public string Emotion { get; set; }

        public string Source { get; set; }

        public DateTime DateCreated { get; set; }
    }
}