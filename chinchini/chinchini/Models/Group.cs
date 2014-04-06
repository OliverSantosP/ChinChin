using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }

        public virtual List<UserGroup> UserGroups { get; set; }
    }
}