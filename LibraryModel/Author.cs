using MongoDB.Bson;

namespace BookApp
{
    public class Author
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}