using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5_Final_u20494000.Models
{
    public class Borrowed

    {
        internal object studentId;

        public int BorrowID { get; set; }
        public int StudentID { get; set; }

        public int BookID { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime BroughtDate { get; set; }
    }
}