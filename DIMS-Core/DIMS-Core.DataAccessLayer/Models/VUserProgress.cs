using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Models
{
    public class VUserProgress
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string TrackNote { get; set; }
        public DateTime TrackDate { get; set; }
    }
}
