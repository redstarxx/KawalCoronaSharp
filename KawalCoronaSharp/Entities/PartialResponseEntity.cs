using Newtonsoft.Json;
using KawalCoronaSharp.Enums;

namespace KawalCoronaSharp.Entities
{
    /// <summary>
    /// Represents the global COVID-19 statistic for the given <see cref="DataType" />.
    /// </summary>
    public class PartialResponseEntity
    {
        /// <summary>
        /// Gets the name of the requested <see cref="DataType" />.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the statistic in number for the requested <see cref="DataType" />.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; internal set; }
    }
}
