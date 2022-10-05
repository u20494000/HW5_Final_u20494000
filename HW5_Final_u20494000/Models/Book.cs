using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5_Final_u20494000.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public int Point { get; set; }
        public int AuthorID { get; set; }
        public int TypeID { get; set; }
    }
}