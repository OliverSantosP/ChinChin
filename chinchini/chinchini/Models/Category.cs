using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        
        public virtual List<Project> Projects { get; set; }
    }
}