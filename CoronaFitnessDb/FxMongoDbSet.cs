using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CoronaFitnessDb
{
    public class FxMongoDbSet<T> : IQueryable<T> where T : class
    {
        private readonly IMongoQueryable<T> queryable;

        public FxMongoDbSet(IMongoCollection<T> collection)
        {
            this.Collection = collection;
            this.queryable = collection.AsQueryable();
        }

        private IMongoCollection<T> Collection { get; }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var cursor = await this.Collection.FindAsync(expression);
            return cursor.ToList();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            var users = await this.GetAsync(expression);
            return users.SingleOrDefault();
        }

        public Task AddAsync(T entity)
        {
            return this.Collection.InsertOneAsync(entity);
        }

        public Task AddManyAsync(IEnumerable<T> entities)
        {
            return this.Collection.InsertManyAsync(entities);
        }

        public async Task UpdateAsync(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            await this.Collection.UpdateOneAsync(expression, updateDefinition);
        }

        public Task<DeleteResult> RemoveAsync(Expression<Func<T, bool>> expression)
        {
            return this.Collection.DeleteOneAsync(expression);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Expression Expression => this.queryable.Expression;

        public Type ElementType => this.queryable.ElementType;

        public IQueryProvider Provider => this.queryable.Provider;
    }
}