using Microsoft.Extensions.Options;
using MongoDB.Domain.Model;
using MongoDB.Domain.Repository;
using MongoDB.Driver;
using MongoDB.Infrastrutcture.Database;

namespace MongoDB.Infrastrutcture.Repository
{
    public class UserRepository : MongoDbCollection<User>, IUserRepository
    {
        protected override MongoCollectionSettings Settings { get; set; }
        protected override IMongoCollection<User> MongoCollection { get; set; }
        public UserRepository(IOptions<MongoDatabaseConfig> config) : base(config, "User") { }
    }
}
