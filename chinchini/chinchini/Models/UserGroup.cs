using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class UserGroup
    {
        public int UserGroupID { get; set; }
        public virtual ApplicationUser User { get; set; }
        
    }
}