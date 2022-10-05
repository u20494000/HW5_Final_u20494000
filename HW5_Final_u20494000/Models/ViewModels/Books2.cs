using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5_Final_u20494000.Models.ViewModels
{
    public class Books2
    {
        internal int TypeID;
        internal int AuthorID;

        public int BookID { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public int Point { get; set; }
        public string AuthorSurname { get; set; }
        public string TypeName { get; set; }

        public bool Status { get; set; }
        public int TotalBorrowed { get; set; }
        public List<Borrowed2> BorrowedRecords { get; set; }
        public List<Borrowed2> borrowedRecords { get; internal set; }
        public int StudentID { get; set; }
    }
}