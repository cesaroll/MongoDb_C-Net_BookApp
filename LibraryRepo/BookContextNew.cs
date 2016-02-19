using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB;

namespace BookApp
{
    public class BookContextNew
    {
        private IMongoClient client { get; set; }
        private IMongoDatabase db { get; set; }

        public BookContextNew(IMongoClient mc)
        {
            //Connecting to local
            client = mc;

            this.db = client.GetDatabase("bookStore");
            
        }

        public IMongoCollection<Book> Books
        {
            get { return db.GetCollection<Book>("BooksNew"); }
        }

        public IQueryable<Book> QBooks
        {
            get { return db.GetCollection<Book>("BooksNew").AsQueryable(); }
        }

    }
}