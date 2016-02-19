using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookApp
{
    public class Book
    {
        [BsonId]
        public string ISBN { get; set; }
        public string Title { get; set; }
        [BsonIgnoreIfNull]
        public string Author { get; set; }
        [BsonIgnoreIfNull]
        public ObjectId AuthorId { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; }
        public string ImageUrlSmall { get; set; }
        public string ImageUrlMedium { get; set; }
        public string ImageUrlLarge { get; set; }
        [BsonIgnoreIfNull]
        public int? PageCount { get; set; }

    }
}