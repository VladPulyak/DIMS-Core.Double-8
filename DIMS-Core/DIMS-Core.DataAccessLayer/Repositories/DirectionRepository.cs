using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class DirectionRepository : Repository<Direction>
    {
        public DirectionRepository(DIMSContext context) : base(context)
        {
        }
    }
}