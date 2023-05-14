using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Models
{
    public class TaskState
    {
        public TaskState()
        {
            UserTasks = new HashSet<UserTask>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<UserTask> UserTasks { get; set; }
    }
}
