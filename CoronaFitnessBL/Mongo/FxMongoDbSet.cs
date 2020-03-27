using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CoronaFitnessBL.Mongo
{
    public class FxMongoDbSet<T> : IQueryable<T> where T : class
    {
        private readonly IMongoQueryable<T> _queryable;

        public FxMongoDbSet(IMongoCollection<T> collection)
        {
            this.Collection = collection;
            this._queryable = collection.AsQueryable();
        }

        private IMongoCollection<T> Collection { get; }

        public async Task<List<T>> Get(Expression<Func<T, bool>> expression)
        {
            var cursor = await this.Collection.FindAsync(expression);
            return cursor.ToList();
        }

        public Task AddAsync(T entity)
        {
            return this.Collection.InsertOneAsync(entity);
        }

        public Task AddManyAsync(IEnumerable<T> entities)
        {
            return this.Collection.InsertManyAsync(entities);
        }

        public Task<DeleteResult> RemoveAsync(Expression<Func<T, bool>> expression)
        {
            return this.Collection.DeleteOneAsync(expression);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Expression Expression => this._queryable.Expression;

        public Type ElementType => this._queryable.ElementType;

        public IQueryProvider Provider => this._queryable.Provider;
    }
}