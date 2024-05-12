using Dotnet.Mongo.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dotnet.Mongo.MongoServices
{
    public class MongoService : IMongoService
    {
        IMongoCollection<Person> _collection;
        MongoDbContext _context;

        public MongoService(MongoDbContext context)
        {
            _context = context;
            _collection = _context.GetPersonsCollection();
        }

        //insert data
        public async Task<Response<string>> InsertData(Person person)
        {
            Response<string> res = new Response<string>();

            try
            {
                Person _person = new Person()
                {
                    Name = person.Name,
                    Address = person.Address,
                    Age = person.Age,
                };

                await _collection.InsertOneAsync(_person);

                res = new Response<string>()
                {
                    message = "data added",
                    data = person.Name
                };

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                res.message = "error";
                res.data = "error";

                return res;
            }
        }

        //get all data
        public async Task<Response<List<Person>>> GetAll()
        {
            Response<List<Person>> res = new Response<List<Person>>();
            try
            {
                var cursor = await _collection.FindAsync(_ => true);
                var result = await cursor.ToListAsync();

                res.message = "Success";
                res.data = result;
            }
            catch (Exception ex)
            {
                res.message = "Error";
                res.data = null;
            }

            return res;
        }

        //find by id
        public async Task<Response<Person>> GetById(string id)
        {
            Response<Person> res = new Response<Person>();
            try
            {
                var cursor = await _collection.FindAsync(c => c.Id == id);
                var person = await cursor.FirstOrDefaultAsync(); // Retrieve the first matching document

                if (person != null)
                {
                    res.message = "Success";
                    res.data = person; // Assign the retrieved person to res.data
                }
                else
                {
                    res.message = "Error";
                    res.data = null;
                }
            }
            catch (Exception ex)
            {
                res.message = "Error";
                res.data = null;
            }

            return res;
        }

        //delete user
        public async Task<Response<string>> Delete(string id)
        {
            Response<string> res = new Response<string>();
            try
            {
                var cursor = await _collection.DeleteOneAsync(c => c.Id == id);

                if (cursor.DeletedCount > 0)
                {
                    res.message = "Success";
                    res.data = "Person Deleted"; 
                }
                else
                {
                    res.message = "Error";
                    res.data = "Person not found";
                }
            }
            catch
            {
                res.message = "Error";
                res.data = "error in deleting";
            }

            return res;
        }

        //update person
        public async Task<Response<Person>> UpdatePerson(string id, Person updatedPerson)
        {
            Response<Person> res = new Response<Person>();
            try
            {
                var filterData = Builders<Person>.Filter.Eq("_id", ObjectId.Parse(id));

                //updated person data
                var update = Builders<Person>.Update
                .Set("Name", updatedPerson.Name)
                .Set("Address", updatedPerson.Address)
                .Set("Age", updatedPerson.Age);

                //accepts 2 parameter , old and new one
                var result = await _collection.UpdateOneAsync(filterData, update);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    res.message = "Success";
                    res.data = updatedPerson;
                }
                else
                {
                    res.message = "Error";
                    res.data = null;
                }
            }
            catch (Exception ex)
            {
                res.message = "Error";
                res.data = null;
            }

            return res;
        }

    }
}
