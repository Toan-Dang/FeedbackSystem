using FS.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Models {

    public class AppUser : IdentityUser {

        [MaxLength(50)]
        public string FullName { set; get; }

        [MaxLength(255)]
        public string Address { set; get; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { set; get; }

        [MaxLength(50)]
        public override string UserName { set; get; }

        [MaxLength(50)]
        public string Password { set; get; }

        [MaxLength(50)]
        public string Role { set; get; }

        public List<Enrollment> Enrollments { get; set; }
    }
}