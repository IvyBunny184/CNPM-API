using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class Hall
    {
        [Key]
        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string ID { get; set; }

        [MaxLength(50, ErrorMessage = "{0} is too long")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [MaxLength(8, ErrorMessage = "{0} is too long")]
        [MinLength(8, ErrorMessage = "{0} is too short")]
        [Required(ErrorMessage = "{0} is required")]
        public string TypeID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0,int.MaxValue, ErrorMessage = "{0} is greater than {1}")]
        public int MaxTables { get; set; }

        public string Note { get; set; }

        public string Describe { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0.0, float.MaxValue, ErrorMessage = "{0} must be greater than {1}")]
        public float Price { get; set; }

        [ForeignKey("TypeID")]
        public virtual TypeOfHall TypeOfHall { get; set; }

        public virtual ICollection<ImageOfHall> Images { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
