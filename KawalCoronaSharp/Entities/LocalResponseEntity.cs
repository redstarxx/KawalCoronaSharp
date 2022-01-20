using Newtonsoft.Json;

namespace KawalCoronaSharp.Entities
{
    public class LocalResponseEntity
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; internal set; }

        [JsonProperty("positif", NullValueHandling = NullValueHandling.Ignore)]
        public string Positives { get; internal set; }

        [JsonProperty("sembuh", NullValueHandling = NullValueHandling.Ignore)]
        public string Recovered { get; internal set; }

        [JsonProperty("meninggal", NullValueHandling = NullValueHandling.Ignore)]
        public string Deceased { get; internal set; }

        [JsonProperty("dirawat", NullValueHandling = NullValueHandling.Ignore)]
        public string Hospitalised { get; internal set; }
    }
}
