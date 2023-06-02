using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTask = System.Threading.Tasks.Task;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskRepository : Repository<Task>
    {
        public TaskRepository(DIMSContext context) : base(context)
        {
            Context = context;
        }

        public DIMSContext Context { get; }

        public override async AsyncTask Delete(int taskId)
        {
            await Context.Database.ExecuteSqlRawAsync("execute DeteleTask @TaskId", taskId);
        }
    }
}
