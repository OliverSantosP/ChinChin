using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class PitchComment
    {
        [Key]
        public int PitchCommentID { get; set; }
        [Required]
        public string Title { get; set; }
        
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        public DateTime Timespan { get; set; }

        [ForeignKey("ParentComment")]
        public int ReplyID { get; set; }

        public int PitchID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual PitchComment ParentComment { get; set; }


    }
}