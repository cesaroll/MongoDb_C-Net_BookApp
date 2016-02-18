using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp;
using MongoDB.Driver;

namespace BookQueries
{
    class Queries
    {
        private BookContext bookCtx;

        public IQueryable<Book> Books
        {
            get { return bookCtx.QBooks; }
        }

        public Queries(BookContext ctx)
        {
            bookCtx = ctx;
        }

        public void DisplayTotal()
        {
            var count = bookCtx.Books.AsQueryable().Count();

            Console.WriteLine("Total Count: {0}", count);
        }

        public void DisplayByAuthor(string author)
        {
            var books = Books.Where(b => b.Author == author);

            Console.WriteLine("\n{0} is Author of {1} books:", author, books.Count());

            foreach (var book in books)
            {
                Console.WriteLine("\t{0}", book.Title);
            }

        }

        public void DisplayByPublicationYear(int year)
        {
            var books = Books.Where(b => b.PublicationYear == year);

            Console.WriteLine("\nBooks published in {0}: {1}", year, books.Count());

            foreach (var book in books)
            {
                Console.WriteLine("\t{0} - {1}", book.Author, book.Title);
            }
        }
    }
}
