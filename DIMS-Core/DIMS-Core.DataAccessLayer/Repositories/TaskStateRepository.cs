using DIMS_Core.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTask = System.Threading.Tasks.Task;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskStateRepository : Repository<TaskState>
    {
        public TaskStateRepository(DIMSContext context) : base(context)
        {
        }
    }
}
