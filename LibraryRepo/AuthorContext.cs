using System.Linq;
using MongoDB.Driver;

namespace BookApp
{
    public class AuthorContext
    {
        private IMongoClient client { get; set; }
        private IMongoDatabase db { get; set; }

        public AuthorContext(IMongoClient client)
        {
            this.client = client;

            this.db = client.GetDatabase("bookStore");
        }

        public IMongoCollection<Author> Authors
        {
            get { return db.GetCollection<Author>("Authors"); }
        }

        public IQueryable<Author> QAuthors
        {
            get { return db.GetCollection<Author>("Authors").AsQueryable(); }
        }
    }
}