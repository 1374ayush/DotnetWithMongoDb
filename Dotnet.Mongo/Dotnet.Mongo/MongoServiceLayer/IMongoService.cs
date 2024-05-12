using Dotnet.Mongo.Model;

namespace Dotnet.Mongo.MongoServices
{
    public interface IMongoService
    {
        Task<Response<List<Person>>> GetAll();
        Task<Response<string>> InsertData(Person person);
        Task<Response<Person>> GetById(string id);
        Task<Response<string>> Delete(string id);
        Task<Response<Person>> UpdatePerson(string id, Person updatedPerson);
    }
}