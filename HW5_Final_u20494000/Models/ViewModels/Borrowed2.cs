using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5_Final_u20494000.Models.ViewModels
{
    public class Borrowed2
    {
        public string StudentName { get; set; }

        public Nullable<int> BorrowID { get; set; }

        public DateTime TakenDate { get; set; }

        public Nullable<DateTime> BroughtDate { get; set; }

    }
}