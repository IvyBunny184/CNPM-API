using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class ListServices
    {
        [Key, Column(Order = 1)]
        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string BookingID { get; set; }

        [Key, Column(Order = 1)]
        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string ServiceID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} is greater than {1}")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0.0, float.MaxValue, ErrorMessage = "{0} must be greater than {1}")]
        public float Price { get; set; }

        [ForeignKey("BookingID")]
        public virtual Booking Booking { get; set; }

        [ForeignKey("ServiceID")]
        public virtual Service Service { get; set; }
    }
}
