using Newtonsoft.Json;

namespace KawalCoronaSharp.Entities
{
    /// <summary>
    /// Represents Indonesian COVID-19 statistics.
    /// </summary>
    public class LocalResponseEntity
    {
        /// <summary>
        /// Gets the name of the country.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; internal set; }

        /// <summary>
        /// Gets the number of confirmed cases of the country.
        /// </summary>
        [JsonProperty("positif", NullValueHandling = NullValueHandling.Ignore)]
        public string Positives { get; internal set; }

        /// <summary>
        /// Gets the number of the recovered patients of the country.
        /// </summary>
        [JsonProperty("sembuh", NullValueHandling = NullValueHandling.Ignore)]
        public string Recovered { get; internal set; }

        /// <summary>
        /// Gets the number of the deaths of the country.
        /// </summary>
        [JsonProperty("meninggal", NullValueHandling = NullValueHandling.Ignore)]
        public string Deceased { get; internal set; }

        /// <summary>
        /// Gets the number of the hospitalised patients of the country.
        /// </summary>
        [JsonProperty("dirawat", NullValueHandling = NullValueHandling.Ignore)]
        public string Hospitalised { get; internal set; }
    }
}
