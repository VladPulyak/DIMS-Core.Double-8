using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskRepository : Repository<Models.Task>
    {
        public TaskRepository(DIMSContext context) : base(context)
        {            
        }
    }
}
