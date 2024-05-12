using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Dotnet.Mongo.Model
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
