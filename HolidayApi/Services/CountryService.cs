using Newtonsoft.Json;
using HolidayApi.Models;

namespace HolidayApi.Services
{
    public class CountryService
    {
        public async Task<IEnumerable<Country>> GetCountries()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://kayaposoft.com/enrico/json/v2.0?action=getSupportedCountries"))
                {
                    using (HttpContent content = response.Content)
                    {
                        var result = JsonConvert.DeserializeObject<IEnumerable<Country>>(await content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
        }
    }
}
