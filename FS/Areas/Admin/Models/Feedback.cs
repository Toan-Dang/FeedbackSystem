using FS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Feedback {

        [Key]
        public int FeedbackId { get; set; }

        public string Title { get; set; }
        public string AdminID { get; set; }

        [ForeignKey("AdminID")]
        public AppUser Id { get; set; }

        //public Module Module { get; set; }

        public bool IsDeleted { get; set; }
        public int TypeFeedbackId { get; set; }

        [ForeignKey("TypeFeedbackId")]
        public TypeFeedback TypeFeedbacks { get; set; }

        public List<Feedback_Question> Feedback_Questions { get; set; }
    }
}