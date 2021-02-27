using FS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class Assignment {

        [Display(Name = "Class Name")]
        public int ClassID { get; set; }

        [Display(Name = "Module Name")]
        public int ModuleID { get; set; }

        [Display(Name = "Trainer Name")]
        public string TrainerID { get; set; }

        public string RegistrationCode { get; set; }

        [ForeignKey("ClassID")]
        public Class Class { get; set; }

        [ForeignKey("ModuleID")]
        public Module Module { get; set; }

        [ForeignKey("TrainerID")]
        public AppUser Admin { get; set; }
    }
}