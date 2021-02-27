using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Class {

        [Key]
        public int ClassID { get; set; }

        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}