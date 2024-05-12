using Dotnet.Mongo.Model;
using Dotnet.Mongo.MongoServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Dotnet.Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IMongoCollection<Person> _collection;
        private IMongoService _mongoService;
        MongoDbContext _context;

        public ValuesController(MongoDbContext context, IMongoService mongoService) { 
            _context = context;
            _collection = _context.GetPersonsCollection();
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var res = await _mongoService.GetAll();
            if (res.message == "Success") return Ok(res);
            else return BadRequest(res);
        }

        [HttpPost]
        public async Task<IActionResult> PostData(Person person)
        {
            var res = await _mongoService.InsertData(person);
            if (res.message == "Success") return Ok(res);
            else return BadRequest(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var res = await _mongoService.GetById(id);
            if (res.message == "Success") return Ok(res);
            else return BadRequest(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var res = await _mongoService.Delete(id);
            if (res.message == "Success") return Ok(res);
            else return BadRequest(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson(string id,  Person person)
        {
            var res = await _mongoService.UpdatePerson(id, person);
            if (res.message == "Success") return Ok(res);
            else return BadRequest(res);
        }
    }
}
