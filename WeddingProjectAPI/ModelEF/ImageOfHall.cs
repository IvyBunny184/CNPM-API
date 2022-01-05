using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class ImageOfHall
    {
        [Key]
        [Required(ErrorMessage = "{0} is required")]
        public string Url { get; set; }

        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string HallID { get; set; }

        [ForeignKey("HallID")]
        public virtual Hall Hall { get; set; }
    }
}
