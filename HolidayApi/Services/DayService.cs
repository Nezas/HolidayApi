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
                        var result = JsonConvert.DeserializeObject<JToken>(await content.ReadAsStringAsync());
                        Console.WriteLine(result);
                        return result["isPublicHoliday"].ToString();
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
                        var result = JsonConvert.DeserializeObject<JToken>(await content.ReadAsStringAsync());
                        return result["isWorkDay"].ToString();
                    }
                }
            }
        }
    }
}
