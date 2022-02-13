namespace HolidayApi.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CountryCode { get; set; }
        public ICollection<Region> Regions { get; set; }
        public ICollection<HolidayType> HolidayTypes { get; set; }
        public string FullName { get; set; }
        public FromDate FromDate { get; set; }
        public ToDate ToDate { get; set; }
    }
}
