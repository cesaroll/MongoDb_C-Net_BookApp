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
            //var connectionString = "mongodb://192.168.187.137:27020";
            //var connectionString = "mongodb://192.168.187.137:27017";
            //var connectionString = "mongodb://192.168.187.137:27017/admin";

            //Connecting to local
            client = new MongoClient();
            

            /*
            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("MONGODB", 27017);

            var credential = MongoCredential.CreateMongoCRCredential("admin", "myAdmin", "password");

            settings.Credentials = new[] {credential};
            
            client = new MongoClient(settings);*/

            this.db = client.GetDatabase("bookStore");
            
        }

        public IMongoCollection<Book> Books
        {
            get { return db.GetCollection<Book>("Books"); }
        }
    }
}