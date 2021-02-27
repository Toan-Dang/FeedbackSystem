using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Topic {

        [Key]
        [Display(Name = "Topic Name")]
        public int TopicID { get; set; }

        public string TopicName { get; set; }
    }
}