using Dotnet.Mongo.Model;
using MongoDB.Driver;

namespace Dotnet.Mongo
{
    public class MongoDbContext
    {
        MongoClient client;
        IMongoDatabase database;

        public MongoDbContext(IConfiguration configuration) {

            string connectionString = configuration.GetConnectionString("MongoDBConnection");

            client = new MongoClient(connectionString);
            database = client.GetDatabase("MongoDb");
        }

        //single collection
        public IMongoCollection<Person> GetPersonsCollection()
        {
            return database.GetCollection<Person>("Persons");
        }
    }
}
