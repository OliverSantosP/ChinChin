using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class PitchGallery
    {
        public int PitchGalleryID { get; set; }
        public string ImageURL { get; set; }
        public DateTime? Timespan { get; set; }

        public int StatusID { get; set; }
        public int PitchID { get; set; }

        public virtual Status Status { get; set; }
        public virtual Pitch Pitch { get; set; }
    }
}