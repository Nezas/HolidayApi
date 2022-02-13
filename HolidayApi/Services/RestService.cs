namespace HolidayApi.Services
{
    public static class RestService
    {
        public static async Task<T> Get<T>(string endpoint)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://kayaposoft.com/enrico/json/v2.0?action=" + endpoint))
                {
                    using (HttpContent content = response.Content)
                    {
                        try
                        {
                            var result = JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
                            return result;
                        }
                        catch (NullReferenceException)
                        {
                            return default(T);
                        }
                        catch (JsonSerializationException)
                        {
                            return default(T);
                        }
                    }
                }
            }
        }
    }
}
