using BookManagement.DataAccess.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement.DataAccess.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<Book> GetAllBooks()
        {
            using SqlConnection db = new(_connectionString);
            string sql = @"SELECT Id, Title, Author, ISBN, PublicationYear, Genre FROM Books";
            return db.Query<Book>(sql).ToList();
        }

        public Book GetBookById(int id)
        {
            string sql = "SELECT Id, Title, Author, ISBN, PublicationYear, Genre FROM Books WHERE Id = @Id";
            using SqlConnection db = new(_connectionString);
            return db.QuerySingleOrDefault<Book>(sql, new { Id = id });
        }


        public void AddBook(Book book)
        {
            try
            {
                using SqlConnection db = new(_connectionString);
                string sql = @"
                    INSERT INTO Books (Title, Author, ISBN, PublicationYear, Genre)
                    VALUES (@Title, @Author, @ISBN, @PublicationYear, @Genre)
                ";
                db.Execute(sql, new
                {
                    book.Title,
                    book.Author,
                    book.ISBN,
                    book.PublicationYear,
                    book.Genre
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBook(Book book)
        {
            string sql = @"
                UPDATE Books
                SET Title = @Title, Author = @Author, ISBN = @ISBN, PublicationYear = @PublicationYear, Genre = @Genre
                WHERE Id = @Id
            ";

            using SqlConnection db = new(_connectionString);
            db.Execute(sql, new
            {
                book.Id,
                book.Title,
                book.Author,
                book.ISBN,
                book.PublicationYear,
                book.Genre
            });
        }

        public void DeleteBook(int id)
        {
            string sql = "DELETE FROM Books WHERE Id = @Id";
            using SqlConnection db = new(_connectionString);
            db.Execute(sql, new { Id = id });
        }
    }
}
