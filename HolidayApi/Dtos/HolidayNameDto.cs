using Newtonsoft.Json;

namespace HolidayApi.Dtos
{
    public class HolidayNameDto
    {
        [JsonProperty("Lang")]
        public string Language { get; set; }
        public string Text { get; set; }
    }
}
