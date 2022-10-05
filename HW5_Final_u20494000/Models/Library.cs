using HW5_Final_u20494000.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW5_Final_u20494000.Models
{
    public class Library
    {
        private static Library Occurence;
        public static Library GetLibraryService()
        {
            if (Occurence == null)
            {
                Occurence = new Library();
            }
            return Occurence;
        }
        private SqlConnection DevelopConnection()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder["Data Source"] = ".";
            sqlConnectionStringBuilder["Initial Catalog"] = "Library";
            sqlConnectionStringBuilder["IntergratedSecurity"] = ".";
            return new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        }

        public bool openConnection()
        {
            using (SqlConnection connection = DevelopConnection())
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool CloseConnection()
        {
            using (SqlConnection connection = DevelopConnection())
            {
                try
                {
                    connection.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<Books2> GetBooks()
        {
            List<Books2> bookList = new List<Books2>();
            String command = "SELECT book.[bookId] as bookId ,book.[name] as name ,book.[pagecount] as pagecount ,book.[point] as point, auth.[surname] as authorSurname ,type.[name] typeName,  book.[authorId],book.[typeId] " +
                            "FROM [Library].[dbo].[books] book " +
                            "JOIN [Library].[dbo].[authors] auth on book.authorId = auth.authorId " +
                            "JOIN [Library].[dbo].[types] type on book.typeId = type.typeId";
            using (SqlConnection conn = DevelopConnection())
            {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        SqlDataReader TrackBooks = cmd.ExecuteReader();
                        while (TrackBooks.Read())
                        {
                            Books2 book = new Books2();
                            book.BookID = (int)TrackBooks["bookId"];
                            book.Name = (string)TrackBooks["name"];
                            book.Pages = (int)TrackBooks["pagecount"];
                            book.Point = (int)TrackBooks["point"];
                            book.AuthorID = (int)TrackBooks["authorId"];
                            book.TypeID = (int)TrackBooks["typeId"];
                            book.AuthorSurname = (string)TrackBooks["authorSurname"];
                            book.TypeName = (string)TrackBooks["typeName"];
                            book.Status = true;
                            bookList.Add(book);
                        }
                    }
                    CloseConnection();
                }
                catch
                {

                }
            }
            return bookList;
        }

        private void OpenConnection()
        {
            throw new NotImplementedException();
        }

        public List<Students> GetAllStudents()
        {
            List<Students> studentList = new List<Students>();
            String command = "SELECT * FROM [Library].[dbo].[students]";
            using (SqlConnection conn = DevelopConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    SqlDataReader TrackStudents = cmd.ExecuteReader();
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
                        studentList.Add(student);
                    }
                }
            }
            return studentList;
        }

        public List<Borrowed> GetAllBorrowed()
        {
            List<Borrowed> borrowList = new List<Borrowed>();
            String command = "SELECT * FROM [Library].[dbo].[borrows]";
            using (SqlConnection connection = DevelopConnection())
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(command, connection))
                {
                    SqlDataReader TrackBorrowed = cmd.ExecuteReader();
                    while (TrackBorrowed.Read())
                    {
                        Borrowed borrow = new Borrowed();
                        borrow.BorrowID = (int)TrackBorrowed["borrowId"];
                        borrow.StudentID = (int)TrackBorrowed["studentId"];
                        borrow.BookID = (int)TrackBorrowed["bookId"];
                        borrow.TakenDate = Convert.ToDateTime(TrackBorrowed["takenDate"]);
                        var broughtDate = TrackBorrowed["broughtDate"].ToString();
                        if (broughtDate != "")
                        {
                            borrow.BroughtDate = Convert.ToDateTime(TrackBorrowed["broughtDate"]);
                        }
                        else
                        {
                            borrow.BroughtDate = null;
                        }

                        borrowList.Add(borrow);
                    }
                }
            }
            return borrowList;
        }

        public SelectList GetTypes()
        {
            List<Type> typesList = new List<Type>();
            String command = "SELECT * FROM [Library].[dbo].[types]";
            using (SqlConnection connection = DevelopConnection())
            {
                connection.Open();
                using (SqlCommand commands = new SqlCommand(command, connection))
                {
                    SqlDataReader readTypes = commands.ExecuteReader();
                    while (readTypes.Read())
                    {
                        Type type = new Type();
                        type.TypeID = (int)readTypes["typeId"];
                        type.Name = (string)readTypes["name"];
                        typesList.Add(type);
                    }
                }
            }
            return new SelectList(typesList, "typeId", "name");
        }

        public SelectList GetAuthors()
        {
            List<Authors> authorsList = new List<Authors>();
            String command = "SELECT * FROM [Library].[dbo].[authors]";
            using (SqlConnection connection = DevelopConnection())
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(command, connection))
                {
                    SqlDataReader readAuthors = cmd.ExecuteReader();
                    while (readAuthors.Read())
                    {
                        Authors author = new Authors();
                        author.AuthorID = (int)readAuthors["authorId"];
                        author.Name = (string)readAuthors["name"];
                        authorsList.Add(author);
                    }
                }
            }
            return new SelectList(authorsList, "typeId", "name");
        }
}