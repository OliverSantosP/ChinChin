using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class UserGroup
    {
        public int UserGroupID { get; set; }
        public int GroupID { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }

        public virtual Group Group { get; set; }
        public virtual ApplicationUser User { get; set; }
        
    }
}