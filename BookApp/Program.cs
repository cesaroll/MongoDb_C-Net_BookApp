using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookCtx = new BookContext();

            Console.WriteLine("Insert next number: ");
            var num = Console.ReadLine();

            var book = new Book()
            {
                Title = "MongoDB for C# Developers Vol " + num,
                ISBN = "123456789" + num+num+num+num,
                Publisher = "DevelopMentor"
            };

            bookCtx.Books.InsertOne(book);

            book = bookCtx.Books.AsQueryable().FirstOrDefault();
        }
    }
}
