using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.Domain.Repository
{
    public interface ICrudRepository<T>
    {
        #region Insert
        void InsertOneAsync(T document);

        void InsertManyAsync(IList<T> documents);

        void InsertOne(T document);

        void InsertMany(IList<T> documents);
        #endregion

        #region Select
        IQueryable<T> Filter(Expression<Func<T, bool>> filter = null);
        #endregion

        #region Update

        void ReplaceOne(string fieldFilter, object valueFilter, T valueUpdate);
        void UpdateMany(string fieldFilter, object value, string fieldUpdate, object valueUpdate);

        void UpdateManyAsync(string fieldFilter, object value, string fieldUpdate, object valueUpdate);
        #endregion

        #region Remove
        void Remove(string field, object value);
        #endregion
    }
}
