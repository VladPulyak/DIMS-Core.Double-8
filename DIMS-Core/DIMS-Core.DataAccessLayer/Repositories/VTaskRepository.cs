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
    public class VTaskRepository : IReadOnlyRepository<VTask>
    {
        private readonly DIMSContext _context;
        public VTaskRepository(DIMSContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<VTask> GetAll()
        {
            return _context.VTasks.AsNoTracking();
        }
    }
}
