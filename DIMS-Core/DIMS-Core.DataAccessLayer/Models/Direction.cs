using System;
using System.Collections.Generic;

#nullable disable

namespace DIMS_Core.DataAccessLayer.Models
{
    public partial class Direction
    {
        public Direction()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
