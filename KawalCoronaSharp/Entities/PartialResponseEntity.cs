using Newtonsoft.Json;

namespace KawalCoronaSharp.Entities
{
    public class PartialResponseEntity
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; internal set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; internal set; }
    }
}
