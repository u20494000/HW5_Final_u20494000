using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5_Final_u20494000.Models.ViewModels
{
    public class BorrowedBooks
    {
        public int BookID { get; set; }

        public int StudentID { get; set; }

        public BorrowedBooks(int bookId, int studentId)
        {
            this.BookID = bookId;
            this.StudentID = studentId;
        }
}