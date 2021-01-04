using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDB
{
    public class TestWorker : BackgroundService
    {
        private readonly ILogger<TestWorker> _logger;

        public TestWorker(ILogger<TestWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1000, stoppingToken);

            MongoDbDatabase mongoDbDatabase = new MongoDbDatabase();

            int count = 0;
            TestCollection test;
            while (!stoppingToken.IsCancellationRequested && count <= 20)
            {
                test = new TestCollection()
                {
                    Name = $"Name {++count}",
                    Age = count
                };

                mongoDbDatabase.Insert(test, "test");
            }

            string log = JsonConvert.SerializeObject(mongoDbDatabase.Filter<TestCollection>("test").ToList(), Formatting.Indented);
            //Return all 
            Console.WriteLine($@"Return ALL\n {log}\n");

            //Return specific Return specific value with filter
            log = JsonConvert.SerializeObject(mongoDbDatabase.Filter<TestCollection>("test").Where(w => w.Age == 10).ToList(), Formatting.Indented);
            Console.WriteLine($"Return specific value with filter\n {log}\n");

            Test2 t2 = new Test2()
            {
                Address = "Rua XXXX",
                City = "YYYYY"
            };

            //update value with filter
            mongoDbDatabase.Update<TestCollection>("test", "Age", 13, "Test2", t2);
            log = JsonConvert.SerializeObject(mongoDbDatabase.Filter<TestCollection>("test").Where(w => w.Age == 13).ToList(), Formatting.Indented);
            Console.WriteLine($"Updated value\n{log}\n");

            //Remove with filter
            mongoDbDatabase.Remove<TestCollection>("test", "Age", 15);
            log = JsonConvert.SerializeObject(mongoDbDatabase.Filter<TestCollection>("test").Where(w => w.Age == 15).ToList(), Formatting.Indented);

            Console.WriteLine($"DELETE value\n{log}\n");
        }
    }
}
