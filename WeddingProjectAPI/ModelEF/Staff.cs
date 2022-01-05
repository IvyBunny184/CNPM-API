using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class Staff
    {
        [Key]
        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string ID { get; set; }

        [MaxLength(50, ErrorMessage = "{0} is too long")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime Birth { get; set; }

        [RegularExpression(@"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "{0} is invalid")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool Gender { get; set; }

        [MaxLength(8, ErrorMessage = "{0} is to long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string RoleID { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "{0} is too long")]
        [MinLength(6, ErrorMessage = "{0} is too short")]
        public string Password { get; set; }

        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        [ForeignKey("RoleID")]
        public virtual Permission Permission { get; set; }
    }
}
