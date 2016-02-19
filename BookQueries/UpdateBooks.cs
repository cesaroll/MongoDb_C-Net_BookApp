using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookApp;
using MongoDB.Driver;

namespace BookQueries
{
    public class GenerateNewBooks
    {
        private BookContextNew newBookCtx;
        private AuthorContext autCtx;
        private BookContext bookCtx;
        
        

        public GenerateNewBooks(BookContextNew newBookCtx, AuthorContext autCtx, BookContext bookCtx)
        {
            this.newBookCtx = newBookCtx;
            this.autCtx = autCtx;
            this.bookCtx = bookCtx;
        }

        public void StartGeneration()
        {
            var books = bookCtx.QBooks;
            var authors = autCtx.QAuthors;

            var taskList = new List<Task>();

            var newBooks = new List<Book>();
            long newBooksCount = 0;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (var book in books)
            {
                var author = authors.FirstOrDefault(a => a.Name == book.Author);

                if (author != null)
                {
                    book.Author = null;
                    book.AuthorId = author.Id;

                    newBooks.Add(book);
                    newBooksCount++;

                    if ((newBooksCount % 10000) == 0)
                    {
                        Console.WriteLine("Inserting {0} new books.", newBooksCount);
                        taskList.Add(newBookCtx.Books.InsertManyAsync(newBooks));
                        newBooks = new List<Book>();
                    }

                }

            }

            if (newBooks.Count > 0)
            {
                Console.WriteLine("Inserting {0} new books.", newBooksCount);
                taskList.Add(newBookCtx.Books.InsertManyAsync(newBooks));
                newBooks = new List<Book>();
            }

            //Wait for all tasks to finish
            Console.WriteLine("Waiting for tasks to complete ...");
            Task.WaitAll(taskList.ToArray());

            stopWatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopWatch.Elapsed);
        }
        
        

    }
}