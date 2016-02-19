using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp;
using MongoDB.Driver;

namespace BookQueries
{
    public class GenerateAuthors
    {
        private AuthorContext autCtx;
        private BookContext bookCtx;

        public GenerateAuthors(AuthorContext autCtx, BookContext bookCtx)
        {
            this.autCtx = autCtx;
            this.bookCtx = bookCtx;
        }

        public void StartGeneration()
        {
            var books = bookCtx.QBooks;

            var taskList = new List<Task>();

            var dictAuth = new SortedSet<string>();
            var newAuthors = new List<Author>();
            long newAutCount = 0;
            
            foreach (var book in books)
            {
                if (book.Author != null)
                {
                    //Search
                    if(!dictAuth.Contains(book.Author))
                    {
                        //If not contained then create it
                        dictAuth.Add(book.Author);

                        //Insert Author
                        newAuthors.Add(new Author() {Name = book.Author});
                        newAutCount++;

                        if ((newAutCount % 10000) == 0)
                        {
                            Console.WriteLine("Inserting {0} authors.", newAutCount);
                            taskList.Add(autCtx.Authors.InsertManyAsync(newAuthors));
                            newAuthors = new List<Author>();
                        }

                    }
                }
            }

            if (newAuthors.Count > 0)
            {
                Console.WriteLine("\nInserting {0} authors\n", newAutCount);
                taskList.Add(autCtx.Authors.InsertManyAsync(newAuthors));
                newAuthors = new List<Author>();
            }

            //Wait for all tasks to finish
            Console.WriteLine("Waiting for tasks to complete ...");
            Task.WaitAll(taskList.ToArray());

        }
    }
}