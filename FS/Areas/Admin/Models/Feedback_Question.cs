using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Feedback_Question {
        public int FeedbackID { get; set; }
        public int QuestionID { get; set; }

        [ForeignKey("FeedbackID")]
        public Feedback Feedback { get; set; }

        [ForeignKey("QuestionID")]
        public Question Question { get; set; }
    }
}