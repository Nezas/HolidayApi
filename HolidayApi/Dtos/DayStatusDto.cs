namespace HolidayApi.Dtos
{
    public class DayStatusDto
    {
        public string DayStatus { get; set; }

        public DayStatusDto(string dayStatus)
        {
            DayStatus = dayStatus;
        }
    }
}
