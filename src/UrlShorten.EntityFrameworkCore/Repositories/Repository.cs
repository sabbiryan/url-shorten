using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace UrlShorten.EntityFrameworkCore.Repositories
{
    public class Repository<T, TKey> :  IRepository<T, TKey> where T : class, IEntityBase<TKey>
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public T Get(TKey id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public Task<T> GetAsync(TKey id)
        {
            var findAsync = _dbContext.Set<T>().FindAsync(id);
            return findAsync.AsTask();
        }

        public T Create(T entity)
        {
            entity.CreationTime = DateTime.UtcNow;
            entity.IpAddress = GetLocalIpAddress();

            var entityEntry = _dbContext.Set<T>().Add(entity);

            _dbContext.SaveChanges();

            entity = entityEntry.Entity;

            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.CreationTime = DateTime.UtcNow;
            entity.IpAddress = GetLocalIpAddress();

            var entityEntry = await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            entity = entityEntry.Entity;

            return entity;
        }

        public T Update(T entity)
        {
            entity.ModificationTime = DateTime.UtcNow;
            entity.IpAddress = GetLocalIpAddress();

            _dbContext.Entry(entity).State = EntityState.Modified;

            _dbContext.SaveChanges();

            return entity;
        }

        public Task<T> UpdateAsync(T entity)
        {
            entity.ModificationTime = DateTime.UtcNow;
            entity.IpAddress = GetLocalIpAddress();

            _dbContext.Entry(entity).State = EntityState.Modified;

            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }

        public void Delete(T entity)
        {
            entity.DeletionTime = DateTime.UtcNow;

            //_dbContext.Entry(entity).State = EntityState.Deleted;

            entity.IsDeleted = true;
            entity.DeletionTime = DateTime.Now;
            entity.IpAddress = GetLocalIpAddress();
            _dbContext.Attach(entity).State = EntityState.Modified;

            _dbContext.SaveChanges();
        }

        public Task DeleteAsync(T entity)
        {
            entity.DeletionTime = DateTime.UtcNow;

            //_dbContext.Entry(entity).State = EntityState.Deleted;

            entity.IsDeleted = true;
            entity.DeletionTime = DateTime.Now;
            entity.IpAddress = GetLocalIpAddress();
            _dbContext.Attach(entity).State = EntityState.Modified;
            

            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }

        public void Delete(TKey id)
        {
            var entity = Get(id);

            //_dbContext.Set<T>().Remove(entity);

            entity.IsDeleted = true;
            entity.DeletionTime = DateTime.Now;
            entity.IpAddress = GetLocalIpAddress();
            _dbContext.Attach(entity).State = EntityState.Modified;


            _dbContext.SaveChanges();
        }

        public Task DeleteAsync(TKey id)
        {
            var task = GetAsync(id);
            task.ConfigureAwait(false);

            var entity = task.GetAwaiter().GetResult();

            //_dbContext.Set<T>().Remove(entity);

            entity.IsDeleted = true;
            entity.DeletionTime = DateTime.Now;
            entity.IpAddress = GetLocalIpAddress();
            _dbContext.Attach(entity).State = EntityState.Modified;

            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }


        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return null;
        }


    }
}
