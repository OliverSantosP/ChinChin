using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace chinchini.Models
{
    public class ProjectType
    {
        public int ProjectTypeID { get; set; }
        public string Description { get; set; }
    }
}