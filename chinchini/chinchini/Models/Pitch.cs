using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Pitch
    {
        public int PitchID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string VideoURL { get; set; }
        public string FB { get; set; }
        public string TW { get; set; }


        //public int ProjectID { get; set; }       
        //[ForeignKey("ProjectID")]
        //public virtual Project Project { get; set; }

        public virtual List<PitchGallery> PitchGallery { get; set; }

    }
}