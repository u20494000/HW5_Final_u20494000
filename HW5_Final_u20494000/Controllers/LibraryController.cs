using HW5_Final_u20494000.Models;
using HW5_Final_u20494000.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Type = HW5_Final_u20494000.Models.Type;

namespace HW5_Final_u20494000.Controllers
{
    public class LibraryController : Controller
    {
        //Connection to Library Database:

        SqlConnection connectionstring = new SqlConnection("Data Source=.;Ininitial Catalog=Library;Intergrated Security=True;");

        //Display Book List:
        public static List<Books2> Books = new List<Books2>();

        public object Students { get; private set; }

        //Borrowed book List:
        public static List<Borrowed> Borrows = new List<Borrowed>();
        //Books that are borrowed at the moment:
        public static List<BorrowedBooks> PresentlyBorrowedBooks = new List<BorrowedBooks>();
        //List helps search book and student details
        public static List<Books2> Search = null;

        public static int CheckingBook = 0;





        // GET: Home
        public ActionResult Index()
        {
            //Capture book count:
            //Book count if statement:
            //Catch error and finally close connectionstring:
            if (Books.Count == 0)
            {
                try
                {
                    SqlCommand GetBooks = new SqlCommand("SELECT * book.[bookId] as BookID,book.[name] as Name,book.[pagecount] as Pages,book.[point] as Point,auth.[surname],type.[name] as TypeName FROM [Library].[dbo].[books]book JOIN [Library].[dbo].[authors] auth on book.authorId=auth.authorId JOIN [Library].[dbo].[types] type on book.typeId=type.typeId", connectionstring);
                    connectionstring.Open();
                    SqlDataReader TrackBooks = GetBooks.ExecuteReader();
                    while (TrackBooks.Read())
                    {
                        Books2 book = new Books2();
                        book.BookID = (int)TrackBooks["bookId"];
                        book.Name = (string)TrackBooks["name"];
                        book.Pages = (int)TrackBooks["pagecount"];
                        book.Point = (int)TrackBooks["point"];
                        book.AuthorSurname = (string)TrackBooks["authorSurname"];
                        book.TypeName = (string)TrackBooks["TypeName"];
                        book.Status = true;
                        Books.Add(book);
                        connectionstring.Close();
                        GetAllBorrowed();

                        //Manages where abouts of borrowed books:
                        //for loop that loops through all books:
                        for (int i = 0; i < Borrows.Count; i++)
                        {
                            if (Borrows[i].BroughtDate == null)
                            {
                                PresentlyBorrowedBooks.Add(new BorrowedBooks(Borrows[i].BookID));
                            }


                        }
                        for (int i = 0; i < Borrows.Count; i++)
                        {
                            //for loop for individual borrowed books:
                            for (int j = 0; j < PresentlyBorrowedBooks.Count; j++)
                            {
                                if (Books[i].BookID == PresentlyBorrowedBooks[j].BookID)
                                {
                                    Books[i].Status = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    ViewBag.Message = error.Message;
                }
                finally
                {
                    connectionstring.Close();
                }
            }
            return View();
        }

        //Functions and methods:
        //Method gets the details about all borrowed books and adds borrowed books to library system:
        private void GetAllBorrowed()
        {
            SqlCommand GetBorrowed = new SqlCommand("SELECT * FROM [Library].[dbo].[borrows]", connectionstring);
            connectionstring.Open();
            SqlDataReader TrackBorrowed = GetBorrowed.ExecuteReader();
            while (TrackBorrowed.Read())
            {
                Borrowed borrow = new Borrowed();
                borrow.BorrowID = (int)TrackBorrowed["borrowId"];
                borrow.StudentID = (int)TrackBorrowed["studentId"];
                borrow.BookID = (int)TrackBorrowed["bookId"];
                borrow.TakenDate = (DateTime)TrackBorrowed["takenDate"];
                borrow.BroughtDate = (DateTime)TrackBorrowed["broughtDate"];
                Borrows.Add(borrow);
            }
            connectionstring.Close();


        }
        //Method gets all the student details that enter the library system and adds the details to library system:
        private void GetStudent()
        {
            SqlCommand GetAllStudents = new SqlCommand("SELECT * FROM [Library].[dbo].[students]", connectionstring);
            connectionstring.Open();
            SqlDataReader TrackStudents = GetAllStudents.ExecuteReader();
            while (TrackStudents.Read())
            {
                Students student = new Students();
                student.StudentID = (int)TrackStudents["studentId"];
                student.Name = (string)TrackStudents["name"];
                student.Surname = (string)TrackStudents["surname"];
                student.Birthday = (DateTime)TrackStudents["birthdate"];
                student.Gender = (string)TrackStudents["gender"];
                student.Class = (string)TrackStudents["class"];
                student.Point = (int)TrackStudents["point"];
                Students.Add(student);
            }
            connectionstring.Close();
        }

        //Method helps capture all authors of the books in the library's details and adds it to the library system:
        private SelectList GetAuthors()
        {
            List<Authors> authors = new List<Authors>();
            SqlCommand GetAllAuthors = new SqlCommand("SELECT * FROM [Library].[dbo].[authors]", connectionstring);
            connectionstring.Open();
            SqlDataReader readAuthors = GetAllAuthors.ExecuteReader();
            while (readAuthors.Read())
            {
                Authors author = new Authors();
                author.AuthorID = (int)readAuthors["authorId"];
                author.Name = (string)readAuthors["name"];
                authors.Add(author);
            }
            return new SelectList(authors, "authorId", "name");
        }
        //Method captures book type details and adds it to the library system:
        private SelectList GetBookType()
        {
            List<Type> types = new List<Type>();
            SqlCommand GetAllTypes = new SqlCommand("SELECT * FROM [Library].[dbo].[types]", connectionstring);
            connectionstring.Open();
            SqlDataReader readTypes = GetAllTypes.ExecuteReader();
            while (readTypes.Read())
            {
                Type type = new System.Type();
                types.TypeID = (int)readTypes["typeId"];
                types.Name = (string)readTypes["name"];
                types.Add(type);
            }
            connectionstring.Close();
            return new SelectList(types, "typeId", "name");
        }
        public static List<Borrowed> Borrowed = new List<Borrowed>();
        public static List<Books2> Searched = null;


        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            List<Books2> returnBooks = null;
            try
            {
                Books.Clear();
                Borrows.Clear();
                if (Searched != null)
                {
                    returnBooks = Searched;
                }
                else
                {
                    Books = connectionstring.GetAllBooks();
                    Students = connectionstring.GetAllStudents();
                    Borrows = connectionstring.GetAllBorrowed();
                    for (int i = 0; i < Borrows.Count; i++)
                    {
                        if (Borrows[i].BroughtDate == null)
                        {
                            Books2 book = Books.Where(x => x.BookID == Borrowed[i].BookID).FirstOrDefault();
                            book.Status = false;
                            book.StudentID = (int)Borrowed[i].StudentID;
                        }
                    }
                    returnBooks = Books;
                }


                ViewBag.Types = connectionstring.GetBookType();

                ViewBag.Authors = connectionstring.GetAuthors();

            }
            catch (Exception message)
            {
                ViewBag.Message = message.Message;
            }
            finally
            {
                object p = connectionstring.CloseConnection();
            }

            return View(returnBooks);
        }

        [HttpGet]
        public ActionResult Clear()
        {
            Searched.Clear();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Search(string name, int? typeId, int? authorId)
        {
            try
            {
                if (Searched != null)
                {
                    Searched.Clear();
                }
                if (name != "" && typeId != null && authorId != null)
                {
                    //Searches book details that consists only of the ID and name of the author and the genre or type of book they wrote:
                    GetAllBooks();
                    Searched = Books.Where(x => x.Name == name && x.TypeID == typeId && x.AuthorID == authorId).ToList();
                }
                else if (name != "" && typeId != null && authorId == null)
                {

                }
                else if (name != "" && typeId == null && authorId != null)
                {

                }
                else if (name == "" && typeId != null && authorId != null)




                else if (name == "" && typeId == null && authorId != null)
                        {

                        }
                        else if (name == "" && typeId != null && authorId == null)
                        {

                            Searched = Books.Where(x => x.TypeID == typeId).ToList();
                        }
                        else if (name != "" && typeId == null && authorId == null)
                        {
                            //  
                        }
                        else
                        {
                            TempData["Message"] = "Search Field Empty";
                        }
            }
            catch (Exception message)
            {
                TempData["Message"] = message;
            }

            return RedirectToAction("Index");
        }
        //Method to generate and display all books:
        private void GetAllBooks()
        {
            throw new NotImplementedException();
        }
        //Method to capture all book details:
        [HttpGet]
        public ActionResult Details(int bookId)
        {
            Books2 bookInList = Books.Where(x => x.BookID == bookId).FirstOrDefault();
            if (bookInList != null)
            {
                var AllRecordsOfBookInBorrows = Borrows.Where(x => x.BookID == bookId).ToList();
                bookInList.TotalBorrowed = AllRecordsOfBookInBorrows.Count();
                List<Borrowed2> RecordOfBorrowed = new List<Borrowed2>();
                for (int i = 0; i < AllRecordsOfBookInBorrows.Count(); i++)
                {
                    Borrowed2 record = new Borrowed2();
                    record.BorrowID = AllRecordsOfBookInBorrows[i].BorrowID;
                    record.StudentName = Students.Where(x => x.StudentID == AllRecordsOfBookInBorrows[i].StudentID).FirstOrDefault().Name;
                    record.TakenDate = AllRecordsOfBookInBorrows[i].TakenDate;
                    record.BroughtDate = AllRecordsOfBookInBorrows[i].BroughtDate;
                    RecordOfBorrowed.Add(record);
                }
                bookInList.borrowedRecords = RecordOfBorrowed;
            }
            else
            {
                ViewBag.Message = "Book Not Found";
            }
            return View(bookInList);
        }

        [HttpGet]
        public ActionResult ViewStudents(int bookId)
        {
            Books2 book = Books.Where(x => x.BookID == bookId).FirstOrDefault();
            ViewBag.Status = book.Status;
            if (book.StudentID != 0)
            {
                ViewBag.studentId = book.StudentID;
            }
            else
            {
                ViewBag.studentId = 0;
            }
            CheckingBook = 0;
            CheckingBook = bookId;
            return View(Students);
        }
    }
}
