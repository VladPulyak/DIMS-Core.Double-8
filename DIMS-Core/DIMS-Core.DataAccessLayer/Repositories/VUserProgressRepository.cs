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
    public class VUserProgressRepository : IReadOnlyRepository<VUserProgress>
    {
        private readonly DIMSContext _context;

        public VUserProgressRepository(DIMSContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<VUserProgress> GetAll()
        {
            return _context.VUserProgresses.AsNoTracking();
        }
    }
}
