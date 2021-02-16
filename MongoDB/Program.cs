using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Domain.Repository;
using MongoDB.Infrastrutcture.Repository;

namespace MongoDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IUserRepository, UserRepository>();

                    services.AddHostedService<UserWorker>();

                    services.Configure<MongoDatabaseConfig>(hostContext.Configuration.GetSection("MongoDatabaseConfig"));
                });
    }
}
