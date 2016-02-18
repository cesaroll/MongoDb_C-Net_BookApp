using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB;

namespace BookApp
{
    public class BookContext
    {
        private IMongoClient client { get; set; }
        private IMongoDatabase db { get; set; }

        public BookContext()
        {
            //Connecting to local
            client = new MongoClient();

            this.db = client.GetDatabase("bookStore");
            
        }

        public IMongoCollection<Book> Books
        {
            get { return db.GetCollection<Book>("Books"); }
        }

        public IQueryable<Book> QBooks
        {
            get { return db.GetCollection<Book>("Books").AsQueryable(); }
        }

    }
}