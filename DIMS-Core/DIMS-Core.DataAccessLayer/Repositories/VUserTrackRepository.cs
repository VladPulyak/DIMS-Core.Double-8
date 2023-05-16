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
    public class VUserTrackRepository : IReadOnlyRepository<VUserTrack>
    {
        private readonly DIMSContext _context;
        public VUserTrackRepository(DIMSContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<VUserTrack> GetAll()
        {
            return _context.VUserTracks.AsNoTracking();
        }
    }
}
