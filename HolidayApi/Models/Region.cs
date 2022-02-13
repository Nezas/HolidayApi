namespace HolidayApi.Models
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionId { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public Country Country { get; set; }
    }
}
