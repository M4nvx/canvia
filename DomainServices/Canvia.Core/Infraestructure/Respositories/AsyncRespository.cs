using Canvia.Core.Infraestructure.Respositories.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Canvia.Core.Infraestructure.Respositories
{
    public class AsyncRespository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        #region Fields

        protected readonly DbContext Context;

        #endregion

        #region Contructor

        public AsyncRespository(DbContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Context

        public TContext DbContext<TContext>() where TContext : DbContext
        {
            return this.Context as TContext;
        }

        #endregion

        public async Task<TEntity> GetById(int id)
        {
            return await this.Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetById(string id)
        {
            return await this.Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await this.Context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllOrder(Func<TEntity, object> order, ListSortDirection sortDirection)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                return await this.Context.Set<TEntity>().OrderBy(c => order).ToListAsync().ConfigureAwait(false);
            }
            else
            {
                return await this.Context.Set<TEntity>().OrderByDescending(c => order).ToListAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<TEntity>> Select(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> SelectIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().Where(predicate);
            if (includes != null)
            {
                query = includes.Aggregate(query, (c, include) => c.Include(include));
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> Add(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);
            return await this.Context.SaveChangesAsync();
        }

        public async Task<TEntity> AddAndReturn(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);
            await this.Context.SaveChangesAsync();
            return entity;
        }

        public async Task AddRange(IEnumerable<TEntity> entityList)
        {
            this.Context.Set<TEntity>().AddRange(entityList);
            await this.Context.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            this.Context.Set<TEntity>().Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            return await this.Context.SaveChangesAsync();
        }

        public async Task UpdateEntity(TEntity entitySource, TEntity entityDestiny)
        {
            this.Context.Entry(entityDestiny).CurrentValues.SetValues(entitySource);
            await this.Context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateKeyAndReturn(TEntity entity, object key)
        {
            if (entity == null)
                return null;
            var exist = await this.Context.Set<TEntity>().FindAsync(key);
            if (exist != null)
            {
                this.Context.Entry(exist).CurrentValues.SetValues(entity);
                await this.Context.SaveChangesAsync();
            }
            return exist;
        }

        public async Task Delete(TEntity entity)
        {
            this.Context.Set<TEntity>().Attach(entity);
            this.Context.Set<TEntity>().Remove(entity);
            await this.Context.SaveChangesAsync();
        }

        public async Task<int> DeleteAndReturn(TEntity entity)
        {
            this.Context.Set<TEntity>().Attach(entity);
            this.Context.Set<TEntity>().Remove(entity);
            return await this.Context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<TEntity> entityList)
        {
            var entities = entityList.ToList();
            foreach (var entity in entities)
            {
                this.Context.Set<TEntity>().Attach(entity);
            }
            this.Context.Set<TEntity>().RemoveRange(entities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().Where(predicate);
            if (includes != null)
            {
                query = includes.Aggregate(query, (c, include) => c.Include(include));
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TObject> FirstOrDefaultSelect<TObject>(Expression<Func<TEntity, TObject>> select, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().Where(predicate);
            if (includes != null)
            {
                query = includes.Aggregate(query, (c, include) => c.Include(include));
            }
            return await query.Select(select).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<TEntity>> SelectIncludesWithPagination(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, System.ComponentModel.ListSortDirection sortDirection, int page, int pageSize, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Context.Set<TEntity>().Where(predicate);
            if (includes != null)
            {
                query = includes.Aggregate(query, (c, include) => c.Include(include));
            }

            var currentPage = page > 0 ? page : 1;
            int skipRows = (currentPage - 1) * pageSize;

            if (sortDirection == ListSortDirection.Descending)
            {
                return await query.OrderByDescending(order).Skip(skipRows).Take(pageSize).ToListAsync().ConfigureAwait(false);
            }
            else
            {
                return await query.OrderBy(order).Skip(skipRows).Take(pageSize).ToListAsync().ConfigureAwait(false);
            }
        }


        public async Task<TEntity> ExecuteQuery<TTEntity>(string query, List<System.Data.SqlClient.SqlParameter> parameters)
        {
            var connection = Context.Database.Connection;

            string json = "";
            try
            {
                json = await Queries.ExecuteQueryHelper(query, parameters, connection);
            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return System.Text.Json.JsonSerializer.Deserialize<TEntity>(json);
        }

        public Task<IEnumerable<TEntity>> SelectIncludesWithTake(Expression<Func<TEntity, bool>> predicate, int take, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> SelectIncludesOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FirstOrDefaultOrder(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> order, ListSortDirection sortDirection, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TObject>> SelectObjectWithIncludes<TObject>(Expression<Func<TEntity, TObject>> select, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TObject>> SelectObjectWithIncludes<TKey, TObject>(Expression<Func<TEntity, TObject>> select, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, ListSortDirection sortDirection, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public void Attach(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }
    }
}
