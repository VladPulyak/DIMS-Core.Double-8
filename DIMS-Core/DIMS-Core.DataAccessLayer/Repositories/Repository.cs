using System;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> Set;

        protected Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Set = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(int id)
        {
            if (id == 0)
            {

            }

            object objectFromDB = null;

            if (objectFromDB is null)
            {

            }

            throw new NotImplementedException();
        }

        public Task<TEntity> Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}