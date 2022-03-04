namespace HolidayApi.DAL.Models
{
    public class HolidayType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HolidayTypeId { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public Country Country { get; set; }
    }
}
