using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookCtx = new BookContext();
            long count = 0;

            var books = new List<Book>();

            Console.WriteLine("Loading Books ...");
            
            foreach (var item in GetBookData())
            {
                try
                {
                    var book = new Book()
                    {
                        ISBN = item[0],
                        Title = item[1],
                        Author = item[2],
                        PublicationYear = int.Parse(item[3]),
                        Publisher = item[4],
                        ImageUrlSmall = item[5],
                        ImageUrlMedium = item[6],
                        ImageUrlLarge = item[7],
                    };

                    books.Add(book);
                    count++;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in line: {0}", count+1);
                }

                //Insert every 1,000 records
                if ((count % 1000) == 0)
                {
                    bookCtx.Books.InsertMany(books);
                    Console.WriteLine("Inserting: {0}", count);
                    books = new List<Book>();
                }
            }

            //Insert last if any
            if (books.Count > 0)
            {
                bookCtx.Books.InsertMany(books);
                Console.WriteLine("Inserting: {0}", count);
                books = null;
            }

            Console.WriteLine("Finished!");
            

            // Read some
            /*
            var books = Queryable.Where(bookCtx.Books.AsQueryable(), b => b.PageCount > 50);
            Console.WriteLine("Books with more than 50 pages: {0}", books.Count());
            Console.WriteLine("First One: {0}", books.FirstOrDefault().Title);
            Console.ReadKey();
            */
        }

        public static IEnumerable<string[]> GetBookData()
        {
            string filename = @"D:\Temp\BX-CSV-Dump\BX-Books.csv";
            var bookLines = File.ReadLines(filename);
            return bookLines.Skip(1).Select(s => s.Replace("\"", "").Replace("&amp;", "&").Split(';'));
        }
        
    }
}
