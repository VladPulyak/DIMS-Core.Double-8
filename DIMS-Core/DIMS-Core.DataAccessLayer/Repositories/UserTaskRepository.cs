using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTask = System.Threading.Tasks.Task;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class UserTaskRepository : Repository<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(DIMSContext context) : base(context)
        {
            Context = context;
        }

        public DIMSContext Context { get; }

        public async AsyncTask SetUserTaskAsFail(int userId, int taskId)
        {
            await Context.Database.ExecuteSqlRawAsync("execute SetUserTaskAsFail @UserId, @TaskId", userId, taskId);
        }

        public async AsyncTask SetUserTaskAsSuccess(int userId, int taskId)
        {
            await Context.Database.ExecuteSqlRawAsync("execute SetUserTaskAsSuccess @UserId, @TaskId", userId, taskId);
        }
    }
}
