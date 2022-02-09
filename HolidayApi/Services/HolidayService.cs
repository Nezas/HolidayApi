using HolidayApi.Models;
using Newtonsoft.Json;

namespace HolidayApi.Services
{
    public class HolidayService
    {
        public async Task<IEnumerable<Holiday>> GetHolidaysForYear(string country, int year)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"https://kayaposoft.com/enrico/json/v2.0?action=getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday"))
                {
                    using (HttpContent content = response.Content)
                    {
                        var result = JsonConvert.DeserializeObject<IEnumerable<Holiday>>(await content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
        }
    }
}
