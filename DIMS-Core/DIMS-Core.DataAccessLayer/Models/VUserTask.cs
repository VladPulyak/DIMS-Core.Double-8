using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Models
{
    public class VUserTask
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string StateName { get; set; }
    }
}
