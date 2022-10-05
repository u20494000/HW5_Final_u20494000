using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5_Final_u20494000.Models
{
    public class Students
    {
        public int StudentID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Class { get; set; }

        public int Point { get; set; }

        internal static void Add(Students student)
        {
            throw new NotImplementedException();
        }

        internal static object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}