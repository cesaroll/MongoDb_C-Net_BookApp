using System;
using BookApp;
using MongoDB.Driver;

namespace BookQueries
{
    public class Start
    {
        static void Main(string[] args)
        {
            
            var mongoClient = new MongoClient();
            var bookCtx = new BookContext(mongoClient);
            /*
            var query = new Queries(bookCtx);

            query.DisplayTotal();

            query.DisplayByAuthor("Ann Beattie");
            query.DisplayByPublicationYear(2005);

            
            var genAuth = new GenerateAuthors(new AuthorContext(mongoClient), bookCtx);
            genAuth.StartGeneration();

            var bookCtxNew = new BookContextNew(mongoClient);
             * */
            var authCtx = new AuthorContext(mongoClient);
            /*
            var genNewBooks = new GenerateNewBooks(bookCtxNew, authCtx, bookCtx);
            genNewBooks.StartGeneration();
            */
            /*
             * Not a good option, too slow
             * Better let the Mongo driver to work instead
            var genNewBooks = new PGenerateNewBooks();
            genNewBooks.StartGenerationParallel();
             * */



            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        } 
    }
}