using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>
    {
        public UserProfileRepository(DIMSContext context) : base(context)
        {
        }
    }
}