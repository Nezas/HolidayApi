namespace HolidayApi.Dtos
{
    public class DateDto
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public DateDto(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
    }
}
