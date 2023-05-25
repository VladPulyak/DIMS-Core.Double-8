using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> 
        where TEntity : class
    {
        private readonly DIMSContext _context;
        private readonly DbSet<TEntity> _set;

        protected ReadOnlyRepository(DIMSContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set.AsNoTracking();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
