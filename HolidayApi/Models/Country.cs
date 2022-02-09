namespace HolidayApi.Models
{
    public class Country
    {
        public string? CountryCode { get; set; }
        public string[]? Regions { get; set; }
        public string[]? HolidayTypes { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Date? FromDate { get; set; }
        public Date? ToDate { get; set; }

    }
}
