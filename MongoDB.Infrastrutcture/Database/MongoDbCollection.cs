using Microsoft.Extensions.Options;
using MongoDB.Domain.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.Infrastrutcture.Database
{
    public abstract class MongoDbCollection<T> : ICrudRepository<T>
    {
        protected abstract MongoCollectionSettings Settings { get; set; }
        protected abstract IMongoCollection<T> MongoCollection { get; set; }
        protected MongoDbCollection(IOptions<MongoDatabaseConfig> config, string collectionName)
        {
            Settings = new MongoCollectionSettings();
            MongoCollection =
                new MongoClient(config.Value.ConnectionString)
                .GetDatabase(config.Value.Database)
                .GetCollection<T>(collectionName, Settings);
        }

        #region Create
        public void InsertOneAsync(T document)
        {
            MongoCollection.InsertOneAsync(document);
        }

        public void InsertManyAsync(IList<T> documents)
        {
            MongoCollection.InsertManyAsync(documents);
        }

        public void InsertOne(T document)
        {
            MongoCollection.InsertOne(document);
        }

        public void InsertMany(IList<T> documents)
        {
            MongoCollection.InsertMany(documents);
        }
        #endregion

        #region Select
        public IQueryable<T> Filter(Expression<Func<T, bool>> filter = null)
        {
            var query = MongoCollection.AsQueryable();

            if(filter != null)
                return query.Where(filter);

            return query;
        }
        #endregion

        #region Update
        public void UpdateMany(string fieldFilter, object value, string fieldUpdate, object valueUpdate)
        {
            var filter = Builders<T>.Filter.Eq(fieldFilter, value);
            var update = Builders<T>.Update.Set(fieldUpdate, valueUpdate);

            MongoCollection.UpdateMany(filter, update);
        }

        public void UpdateManyAsync(string fieldFilter, object value, string fieldUpdate, object valueUpdate)
        {
            var filter = Builders<T>.Filter.Eq(fieldFilter, value);
            var update = Builders<T>.Update.Set(fieldUpdate, valueUpdate);

            MongoCollection.UpdateManyAsync(filter, update);
        }
        #endregion

        #region Remove
        public void Remove(string field, object value)
        {
            var filter = Builders<T>.Filter.Eq(field, value);
            MongoCollection.DeleteMany(filter);
        }
        #endregion
    }
}
