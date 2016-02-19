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
    public class PGenerateNewBooks
    {
        //Not a good option, too slow

        private ConcurrentQueue<Book> NewBooks;
        private volatile bool readFinished = false;

        public PGenerateNewBooks()
        {
            NewBooks = new ConcurrentQueue<Book>();
        }

        public void StartGenerationParallel()
        {
            

            var stopWatch = new Stopwatch();
            stopWatch.Start();


            (new Task(Read)).Start();
            

            Write();

            stopWatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopWatch.Elapsed);

        }

        private async void Read()
        {
            var mongoClient = new MongoClient();
            var bookCtx = new BookContext(mongoClient);
            var authCtx = new AuthorContext(mongoClient);

            var books = bookCtx.QBooks.AsParallel();
            var authors = authCtx.QAuthors.AsParallel();

            books.ForAll(b =>
            {
                var author = authors.FirstOrDefault(a => a.Name == b.Author);

                if (author != null)
                {
                    b.Author = null;
                    b.AuthorId = author.Id;

                    NewBooks.Enqueue(b);

                }

            });

            readFinished = true;

        }

        private void Write()
        {
            var mongoClient = new MongoClient();
            var bookCtxNew = new BookContextNew(mongoClient);
            
            int newBooksCount = 0;

            Book book;
            var booksToSave = new List<Book>();
            var taskList = new List<Task>();


            while (!readFinished)
            {
                Thread.Sleep(10000);

                while (NewBooks.TryDequeue(out book))
                {
                    booksToSave.Add(book);
                    newBooksCount++;

                    if ((newBooksCount % 10000) == 0)
                    {
                        taskList.Add(bookCtxNew.Books.InsertManyAsync(booksToSave));
                        Console.WriteLine("Inserting {0} new books.", newBooksCount);
                        booksToSave = new List<Book>();
                    }
                }

                if (booksToSave.Count > 0)
                {
                    Console.WriteLine("Inserting {0} new books.", newBooksCount);
                    taskList.Add(bookCtxNew.Books.InsertManyAsync(booksToSave));
                    booksToSave = new List<Book>();
                }
                
            }


            

            //Wait for all tasks to finish
            Console.WriteLine("Waiting for tasks to complete ...");
            Task.WaitAll(taskList.ToArray());

        }
    }
}