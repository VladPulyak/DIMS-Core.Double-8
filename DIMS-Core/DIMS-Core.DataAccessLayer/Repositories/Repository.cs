using System;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.Common.Exceptions;
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
            return Set.AsQueryable();
        }

        public async Task<TEntity> GetById(int id)
        {
            if (id == 0)
            {
                throw new InvalidArgumentException("You inputed invalid argument");
            }
            var objectFromDB = await Set.FindAsync(id);
          
            if (objectFromDB is null)
            {
                throw new ObjectNotFoundException("GetById", "This object isn't found in database"); 
            }
          
            return objectFromDB;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var obj = await Set.AddAsync(entity);
            return obj.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return Set.Update(entity).Entity;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            Set.Remove(entity);
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}