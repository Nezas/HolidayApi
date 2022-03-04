namespace HolidayApi.DAL.Models
{
    public class FromDate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DateId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public string CountryCode { get; set; }
        public Country Country { get; set; }
    }
}
