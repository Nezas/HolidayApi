using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HolidayApi.Models
{
    public class HolidayName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HolidayNameId { get; set; }
        public int HolidayId { get; set; }
        public string CountryCode { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
    }
}
