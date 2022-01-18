using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KawalCoronaSharp.Entities;

namespace KawalCoronaSharp
{
    /// <summary>
    /// An implementation of the Kawal Corona API.
    /// </summary>
    public class KawalCoronaApi
    {
        /// <summary>
        /// Gets the nationwide COVID statistics asynchoronously.
        /// </summary>
        /// <returns>A <see cref="LocalResponseEntity" /> object containing the statistics.</returns>
        public async Task<LocalResponseEntity> GetIndonesianDataAsync()
        {
            string json = string.Empty;
            string endpoint = $"{Endpoints.BASE_URL}{Endpoints.LOCAL}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            var deserializedResponse = JsonConvert.DeserializeObject<List<LocalResponseEntity>>(json);

            return deserializedResponse.First();
        }

        /// <summary>
        /// Gets the COVID statistics for all available countries asynchronously.
        /// </summary>
        /// <returns>A <see cref="List{T}" /> of <see cref="InternationalResponseEntityData" /> objects containing the statistics of each country.</returns>
        public async Task<List<InternationalResponseEntityData>> GetInternationalDataAsync()
        {
            string json = string.Empty;
            string endpoint = $"{Endpoints.BASE_URL}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            var deserializedResponse = JsonConvert.DeserializeObject<List<InternationalResponseEntity>>(json);

            // Sort them into a new object, just to make it neat.
            List<InternationalResponseEntityData> internationalResponseEntityDatas = new List<InternationalResponseEntityData>();

            foreach (var country in deserializedResponse)
            {
                InternationalResponseEntityData countryData = new InternationalResponseEntityData()
                {
                    ObjectId = country.Attributes.ObjectId,
                    Country = country.Attributes.Country,
                    LastUpdated = country.Attributes.LastUpdated,
                    Latitude = country.Attributes.Latitude,
                    Longitude = country.Attributes.Longitude,
                    Confirmed = country.Attributes.Confirmed,
                    Deaths = country.Attributes.Deaths,
                    Recovered = country.Attributes.Recovered,
                    Active = country.Attributes.Active
                };

                internationalResponseEntityDatas.Add(countryData);
            }

            return internationalResponseEntityDatas;
        }

        /// <summary>
        /// Gets the COVID statistics for the given country name.
        /// </summary>
        /// <param name="countryName">The name of the country.</param>
        /// <returns>A <see cref="InternationalResponseEntityData" /> object containing the statistic of the given country name.</returns>
        public async Task<InternationalResponseEntityData> GetCountryDataAsync(string countryName)
        {
            string json = string.Empty;
            string endpoint = $"{Endpoints.BASE_URL}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            var deserializedResponse = JsonConvert.DeserializeObject<List<InternationalResponseEntity>>(json);

            if (!deserializedResponse.Any(x => x.Attributes.Country == countryName))
            {
                throw new ArgumentException($"Name of country is not found in JSON response.", nameof(countryName));
            }

            else
            {
                deserializedResponse.RemoveAll(x => x.Attributes.Country != countryName);

                InternationalResponseEntityData internationalResponseEntity = new InternationalResponseEntityData()
                {
                    ObjectId = deserializedResponse.First().Attributes.ObjectId,
                    Country = deserializedResponse.First().Attributes.Country,
                    LastUpdated = deserializedResponse.First().Attributes.LastUpdated,
                    Latitude = deserializedResponse.First().Attributes.Latitude,
                    Longitude = deserializedResponse.First().Attributes.Longitude,
                    Confirmed = deserializedResponse.First().Attributes.Confirmed,
                    Deaths = deserializedResponse.First().Attributes.Deaths,
                    Recovered = deserializedResponse.First().Attributes.Recovered,
                    Active = deserializedResponse.First().Attributes.Active
                };

                return internationalResponseEntity;
            }
        }
    }
}
