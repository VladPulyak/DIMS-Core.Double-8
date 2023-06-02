using DIMS_Core.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AsyncTask = System.Threading.Tasks.Task;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>
    {
        public UserProfileRepository(DIMSContext context) : base(context)
        {
            Context = context;
        }

        public DIMSContext Context { get; }

        public override async AsyncTask Delete(int userId)
        {
            await Context.Database.ExecuteSqlRawAsync("execute DeteleUser @UserId", userId);
        }
    }
}