namespace HolidayApi.Dtos
{
    public class MaximumFreeDaysDto
    {
        public int MaximumFreeDaysInRow { get; set; }

        public MaximumFreeDaysDto(int maximumFreeDaysInRow)
        {
            MaximumFreeDaysInRow = maximumFreeDaysInRow;
        }
    }
}
