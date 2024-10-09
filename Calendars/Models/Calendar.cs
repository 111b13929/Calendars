using System.ComponentModel.DataAnnotations;
namespace Calendars.Models
{
    public class Calendar
    {
        [Key]
        public int Cid { get; set; }

        [Required]
        public String Cname { get; set; }

        [Required]
        public int Cpriority { get; set; }

        [Required]
        public bool Cfinish { get; set; }

        public String Cmemo { get; set; }
    }
}
