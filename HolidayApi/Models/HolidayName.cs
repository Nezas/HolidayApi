using Newtonsoft.Json;

namespace HolidayApi.Models
{
    public class HolidayName
    {
        [JsonProperty("Lang")]
        public string Language { get; set; }
        public string Text { get; set; }
    }
}
