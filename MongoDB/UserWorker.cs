using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Domain.Model;
using MongoDB.Domain.Repository;
using MongoDB.Infrastrutcture.Repository;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDB
{
    public class UserWorker : BackgroundService
    {
        private readonly ILogger<UserWorker> _logger;
        private readonly IUserRepository _userRepository;

        public UserWorker(ILogger<UserWorker> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1000, stoppingToken);


            int count = 0;
            User user;
            while (!stoppingToken.IsCancellationRequested && count <= 20)
            {
                user = new User()
                {
                    Name = $"Name {++count}",
                    Age = count
                };

                _userRepository.InsertOne(user);
            }

            //Return all
            string log = JsonConvert.SerializeObject(_userRepository.Filter().ToList(), Formatting.Indented);
            Console.WriteLine($@"Return ALL\n {log}\n");

            //Return specific Return specific value with filter
            log = JsonConvert.SerializeObject(_userRepository.Filter(f => f.Age == 10).ToList(), Formatting.Indented);

            Console.WriteLine($@"Return specific value with filter\n {log}\n");

            Address address = new Address()
            {
                StreetAddress = "Rua XXXX",
                City = "YYYYY"
            };

            //update value with filter
            _userRepository.UpdateMany("Age", 13, "Address", address);
            log = JsonConvert.SerializeObject(_userRepository.Filter(f => f.Age == 13).ToList(), Formatting.Indented);
            Console.WriteLine($@"Updated value\n{log}\n");

            //Remove with filter
            _userRepository.Remove("Age", 15);
            log = JsonConvert.SerializeObject(_userRepository.Filter(f => f.Age == 15).ToList(), Formatting.Indented);

            Console.WriteLine($@"DELETE value\n{log}\n");
        }
    }
}
