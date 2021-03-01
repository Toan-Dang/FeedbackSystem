using FS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Enrollment {
        public int ClassID { get; set; }
        public string TraineeID { get; set; }

        [ForeignKey("ClassID")]
        public Class Class { get; set; }

        [ForeignKey("TraineeID")]
        public AppUser TrainerID { get; set; }
    }
}