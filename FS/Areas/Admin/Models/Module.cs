using FS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Module {

        [Key]
        public int ModuleID { get; set; }

        public string AdminId { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "{0} dài {1} đến {2}")]
        public string ModuleName { get; set; }

        //  [Required(ErrorMessage = "please choose ...")]
        public DateTime? StartTime { get; set; }

        //  [Required(ErrorMessage = "please choose ...")]
        public DateTime? EndTime { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? FeedbackStartTime { get; set; }
        public DateTime? FeedbackEndTime { get; set; }
        public int FeedbackID { get; set; }

        [ForeignKey("AdminId")]
        public AppUser AdminID { get; set; }

        [ForeignKey("FeedbackID")]
        [Display(Name = "Feedback Title")]
        public Feedback Feedbacks { get; set; }
    }
}