using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class Bill
    {
        [Key]
        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string ID { get; set; }

        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string BookingID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime DateOfPayment { get; set; }

        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string PaymentType { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0.0, float.MaxValue, ErrorMessage = "{0} must be greater than {1}")]
        public float Fee { get; set; }

        [ForeignKey("PaymentType")]
        public virtual TypeOfPayment TypeOfPayment { get; set; }

        [ForeignKey("BookingID")]
        public virtual Booking Booking { get; set; }
    }
}
