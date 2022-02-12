using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HolidayApi.Models;

namespace HolidayApi.Services
{
    public class DayService
    {
        public DayStatus GetDayStatus(string country, int year, int month, int day)
        {
            var isPublicHoliday = IsPublicHoliday(country, year, month, day);
            var isWorkDay = IsWorkDay(country, year, month, day);

            if (isPublicHoliday.Result == null && isWorkDay.Result == null)
            {
                return null;
            }

            if (isPublicHoliday.Result == "True")
            {
                return new DayStatus("holiday");
            }
            else if (isWorkDay.Result == "True")
            {
                return new DayStatus("workday");
            }
            return new DayStatus("free day");
        }

        public async Task<string> IsPublicHoliday(string country, int year, int month, int day)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"https://kayaposoft.com/enrico/json/v2.0?action=isPublicHoliday&date={day}-{month}-{year}&country={country}"))
                {
                    using (HttpContent content = response.Content)
                    {
                        try
                        {
                            var result = JsonConvert.DeserializeObject<JToken>(await content.ReadAsStringAsync());
                            return result["isPublicHoliday"].ToString();
                        }
                        catch (NullReferenceException)
                        {
                            return null;
                        }
                        catch (JsonSerializationException)
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<string> IsWorkDay(string country, int year, int month, int day)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"https://kayaposoft.com/enrico/json/v2.0?action=isWorkDay&date={day}-{month}-{year}&country={country}"))
                {
                    using (HttpContent content = response.Content)
                    {
                        try
                        {
                            var result = JsonConvert.DeserializeObject<JToken>(await content.ReadAsStringAsync());
                            return result["isWorkDay"].ToString();
                        }
                        catch (NullReferenceException)
                        {
                            return null;
                        }
                        catch (JsonSerializationException)
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public MaximumFreeDays GetMaximumFreeDays(string country, int year)
        {
            IEnumerable<Holiday> holidays = GetHolidaysForYear(country, year).Result;

            if (holidays == null)
            {
                return null;
            }

            // Calculate february days in the month
            int febDays = 28;
            if (year % 4 == 0)
            {
                febDays = 29;
            }

            // Holds array of month converted to days
            int[] monthToDays = new int[15];
            monthToDays[0] = 0;
            monthToDays[1] = 31;
            monthToDays[2] = 31 + febDays;
            int daySum = 31 + febDays;

            for (int i = 3; i <= 12; i++)
            {
                switch (i)
                {
                    case 3:
                        {
                            daySum += 31;
                            break;
                        }
                    case 4:
                        {
                            daySum += 30;
                            break;
                        }
                    case 5:
                        {
                            daySum += 31;
                            break;
                        }
                    case 6:
                        {
                            daySum += 30;
                            break;
                        }
                    case 7:
                        {
                            daySum += 31;
                            break;
                        }
                    case 8:
                        {
                            daySum += 31;
                            break;
                        }
                    case 9:
                        {
                            daySum += 30;
                            break;
                        }
                    case 10:
                        {
                            daySum += 31;
                            break;
                        }
                    case 11:
                        {
                            daySum += 30;
                            break;
                        }
                    case 12:
                        {
                            daySum += 31;
                            break;
                        }
                    default:
                        break;
                }
                monthToDays[i] = daySum;
            }

            // For each holiday in the year, insert its day to freeDays list.
            List<int> freeDays = new();
            foreach (var holiday in holidays)
            {
                // Calculate holiday's day in the year value.
                int dayInYear = holiday.Date.Day + monthToDays[holiday.Date.Month - 1];
                // If the holiday is at Friday or Monday, add Saturday and Sunday in to the free days list as well.
                if (holiday.Date.DayOfWeek == 5)
                {
                    freeDays.Add(dayInYear);
                    freeDays.Add(dayInYear + 1);
                    freeDays.Add(dayInYear + 2);
                }
                else if (holiday.Date.DayOfWeek == 1)
                {
                    freeDays.Add(dayInYear);
                    freeDays.Add(dayInYear - 1);
                    freeDays.Add(dayInYear - 2);
                }
                else
                {
                    freeDays.Add(dayInYear);
                }
            }

            // Remove same days in the free days list and sort it.
            List<int> uniqueDays = freeDays.Distinct().ToList();
            uniqueDays.Sort();

            // Iterate through the list of uniqueDays and search for the maximum free days in the row.
            int maximumFreeDays = 2;
            int freeDaysInRow;
            for (int i = 0; i < uniqueDays.Count - 1; i++)
            {
                freeDaysInRow = 1;
                for (int j = i + 1; j < uniqueDays.Count; j++)
                {
                    if (uniqueDays[i] + freeDaysInRow == uniqueDays[j])
                    {
                        freeDaysInRow++;
                    }
                    else
                    {
                        break;
                    }

                    if (freeDaysInRow > maximumFreeDays)
                    {
                        maximumFreeDays = freeDaysInRow;
                    }
                }
            }

            return new MaximumFreeDays(maximumFreeDays);
        }

        public async Task<IEnumerable<Holiday>> GetHolidaysForYear(string country, int year)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"https://kayaposoft.com/enrico/json/v2.0?action=getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday"))
                {
                    using (HttpContent content = response.Content)
                    {
                        try
                        {
                            var result = JsonConvert.DeserializeObject<IEnumerable<Holiday>>(await content.ReadAsStringAsync());
                            return result;
                        }
                        catch (NullReferenceException)
                        {
                            return null;
                        }
                        catch (JsonSerializationException)
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}
