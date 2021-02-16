using Microsoft.AspNetCore.Mvc;
using MongoDB.Domain.Model;
using MongoDB.Domain.ModelDTO;
using MongoDB.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Find(string id)
        {
            var user = _userRepository.Filter(f => f.Id == id).FirstOrDefault();

            if (user == null)
                return NotFound();
            else
                return Ok(new UserDto(user));
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            var users = _userRepository.Filter().AsEnumerable();

            if (users.Any())
                return Ok(users.Select(user => new UserDto(user)).ToList());
            else
                return NotFound();
        }

        [HttpPost]
        public void Insert(UserDto user)
        {
            _userRepository.InsertOne(new User(user));
        }

        [HttpPut]
        public void Update(UserDto user)
        {
            _userRepository.ReplaceOne("Id", user.Id, new User(user));
        }

        [HttpDelete]
        public void Delete(string idUser)
        {
            _userRepository.Remove("Id", idUser);
        }
    }
}
