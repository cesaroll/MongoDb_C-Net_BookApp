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

            //client = new MongoClient("mongodb://192.168.187.130:27020");
            
            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("192.168.187.130", 27020);
            
            client = new MongoClient(settings);

            this.db = client.GetDatabase("bookStore");
            
        }

        public IMongoCollection<Book> Books
        {
            get { return db.GetCollection<Book>("Book"); }
        }
    }
}