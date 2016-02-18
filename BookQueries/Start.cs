using System;
using BookApp;

namespace BookQueries
{
    public class Start
    {
        static void Main(string[] args)
        {
            var query = new Queries(new BookContext());

            query.DisplayTotal();

            query.DisplayByAuthor("Ann Beattie");
            query.DisplayByPublicationYear(2005);
            Console.ReadKey();

        } 
    }
}