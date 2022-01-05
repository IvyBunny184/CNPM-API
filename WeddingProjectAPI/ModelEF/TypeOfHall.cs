using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class TypeOfHall
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
        [Range(0.0, float.MaxValue, ErrorMessage = "{0} must be greater than {1}")]
        public float MinPrice { get; set; }

        public virtual ICollection<Hall> Halls { get; set; }
    }
}
