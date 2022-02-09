namespace HolidayApi.Models
{
    public class Holiday
    {
        public HolidayDate Date { get; set; }
        public HolidayName[] Name { get; set; }
        public string HolidayType { get; set; }
    }
}
