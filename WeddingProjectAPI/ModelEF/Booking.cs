using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class Booking
    {
        [Key]
        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string ID { get; set; }

        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string HallID { get; set; }

        [MaxLength(50, ErrorMessage = "{0} is too long")]
        [Required(ErrorMessage = "{0} is required")]
        public string GroomName { get; set; }

        [MaxLength(50, ErrorMessage = "{0} is too long")]
        [Required(ErrorMessage = "{0} is required")]
        public string BrideName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime Date { get; set; }

        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string ShiftID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0.0, float.MaxValue, ErrorMessage = "{0} must be greater than {1}")]
        public float Price { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0.0, float.MaxValue, ErrorMessage = "{0} must be greater than {1}")]
        public float Deposit { get; set; }

        public bool IsCancel { get; set; }

        [ForeignKey("HallID")]
        public virtual Hall Hall { get; set; }

        [ForeignKey("ShiftID")]
        public virtual Shift Shift { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual ICollection<ListServices> ListServices { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
