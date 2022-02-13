namespace HolidayApi.Dtos
{
    public class CountryDto
    {
        public string? CountryCode { get; set; }
        public string[]? Regions { get; set; }
        public string[]? HolidayTypes { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateDto? FromDate { get; set; }
        public DateDto? ToDate { get; set; }
    }
}
