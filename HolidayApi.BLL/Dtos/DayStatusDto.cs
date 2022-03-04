namespace HolidayApi.BLL.Dtos
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
