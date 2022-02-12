namespace HolidayApi.Models
{
    public class MaximumFreeDays
    {
        public int MaximumFreeDaysInRow { get; set; }

        public MaximumFreeDays(int maximumFreeDaysInRow)
        {
            MaximumFreeDaysInRow = maximumFreeDaysInRow;
        }
    }
}
