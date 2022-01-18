using Newtonsoft.Json;

namespace KawalCoronaSharp.Entities
{
    public class InternationalResponseEntityData
    {
        /// <summary>
        /// Gets the object ID of the country.
        /// </summary>
        [JsonProperty("OBJECTID", NullValueHandling = NullValueHandling.Ignore)]
        public int ObjectId { get; internal set; }

        /// <summary>
        /// Gets the name of the country / region.
        /// </summary>
        [JsonProperty("Country_Region", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; internal set; }

        /// <summary>
        /// Gets the last updated unix timestamp of the data.
        /// </summary>
        [JsonProperty("Last_Update", NullValueHandling = NullValueHandling.Ignore)]
        public long LastUpdated { get; internal set; }

        /// <summary>
        /// Gets the latitude of the country.
        /// </summary>
        [JsonProperty("Lat", NullValueHandling = NullValueHandling.Ignore)]
        public double? Latitude { get; internal set; }

        /// <summary>
        /// Gets the longitude of the country.
        /// </summary>
        [JsonProperty("Long_", NullValueHandling = NullValueHandling.Ignore)]
        public double? Longitude { get; internal set; }

        /// <summary>
        /// Gets the number of confirmed cases of the country.
        /// </summary>
        [JsonProperty("Confirmed", NullValueHandling = NullValueHandling.Ignore)]
        public int Confirmed { get; internal set; }

        /// <summary>
        /// Gets the number of the deaths of the country.
        /// </summary>
        [JsonProperty("Deaths", NullValueHandling = NullValueHandling.Ignore)]
        public int Deaths { get; internal set; }

        /// <summary>
        /// Gets the number of the recovered patients of the country.
        /// </summary>
        [JsonProperty("Recovered", NullValueHandling = NullValueHandling.Ignore)]
        public int Recovered { get; internal set; }

        /// <summary>
        /// Gets the number of the active cases of the country.
        /// </summary>
        [JsonProperty("Active", NullValueHandling = NullValueHandling.Ignore)]
        public int Active { get; internal set; }
    }

    public class InternationalResponseEntity
    {
        [JsonProperty("attributes")]
        public InternationalResponseEntityData Attributes { get; internal set; }
    }
}
