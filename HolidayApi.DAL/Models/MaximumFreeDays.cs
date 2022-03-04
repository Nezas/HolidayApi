namespace HolidayApi.DAL.Models
{
    public class MaximumFreeDays
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaximumFreeDaysId { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public int MaximumFreeDaysResult { get; set; }
    }
}
