﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class Permission
    {
        [Key]
        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string ID { get; set; }

        [MaxLength(20, ErrorMessage = "{0} is too long")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
