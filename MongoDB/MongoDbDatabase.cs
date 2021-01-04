using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace MongoDB
{
    public class MongoDbDatabase
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly MongoCollectionSettings _settings;
        public MongoDbDatabase()
        {
            _mongoDatabase = new MongoClient("mongodb://localhost:27017").GetDatabase("test");
            _settings = new MongoCollectionSettings();
        }
        
        public void Insert<T>(T document, string collectionName)
        {
            InsertMany(new List<T> { document }, collectionName);
        }

        public IQueryable<T> Filter<T>(string collectionName)
        {
            return _mongoDatabase.GetCollection<T>(collectionName, _settings).AsQueryable<T>();

        }

        public void Update<T>(string collectionName, string field, object value, string fieldUpdate, object valueUpdate)
        {
            var filter = Builders<T>.Filter.Eq(field, value);
            var update = Builders<T>.Update.Set(fieldUpdate, valueUpdate);

            _mongoDatabase.GetCollection<T>(collectionName, _settings).UpdateMany(filter, update);
        }

        public void Remove<T>(string collectionName, string field, object value)
        {
            var filter = Builders<T>.Filter.Eq(field, value);
            _mongoDatabase.GetCollection<T>(collectionName, _settings).DeleteMany(filter);
        }

        private void InsertMany<T>(List<T> documents, string collectionName)
        {
            var collection = _mongoDatabase.GetCollection<T>(collectionName, _settings);
            collection.InsertMany(documents);
        }
    }
}
