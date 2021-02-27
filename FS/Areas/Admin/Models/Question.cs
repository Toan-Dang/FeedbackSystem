using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Question {

        [Key]
        public int QuestionID { get; set; }

        public int TopicID { get; set; }

        [Display(Name = "Question Content")]
        public string QuestionContent { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("TopicID")]
        public Topic Topic { get; set; }

        public List<Feedback_Question> Feedback_Questions { get; set; }
    }
}