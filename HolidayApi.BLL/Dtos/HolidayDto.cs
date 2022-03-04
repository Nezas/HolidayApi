namespace HolidayApi.BLL.Dtos
{
    public class HolidayDto
    {
        public HolidayDateDto Date { get; set; }
        public HolidayNameDto[] Name { get; set; }
        public string HolidayType { get; set; }
    }
}