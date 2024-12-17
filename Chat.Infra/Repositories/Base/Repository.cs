using Microsoft.EntityFrameworkCore;
using Chat.Infra.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Infra.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ChatDbContext Context;
        protected readonly DbSet<T> DbSet;

        protected Repository(ChatDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = context.Set<T>();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            return entity ?? throw new InvalidOperationException($"Entity with id {id} not found");
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T> Add(T entity)
        {
            var result = await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task<T> Update(T entity)
        {
            var result = DbSet.Update(entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }
    }
} 