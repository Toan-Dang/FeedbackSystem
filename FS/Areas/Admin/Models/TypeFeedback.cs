﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Areas.Admin.Models {

    public class TypeFeedback {

        [Key]
        public int TypeId { get; set; }

        public string TypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}