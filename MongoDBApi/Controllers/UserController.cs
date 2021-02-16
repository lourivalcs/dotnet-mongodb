using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Domain.Model;
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
        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public User Find(string idUser)
        {
            return _userRepository.Filter(f => f.Id == idUser).FirstOrDefault();
        }

        [HttpGet]
        public List<User> FindAll()
        {
            return _userRepository.Filter().ToList();
        }

        [HttpPost]
        public void Insert(User user)
        {
            _userRepository.InsertOne(user);
        }

        [HttpPut]
        public void Update(User user)
        {
            _userRepository.ReplaceOne("Id", user.Id, user);
        }

        [HttpDelete]
        public void Delete(string idUser)
        {
            _userRepository.Remove("Id", idUser);
        }
    }
}
