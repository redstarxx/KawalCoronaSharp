using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KawalCoronaSharp.Entities;
using KawalCoronaSharp.Enums;

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
            string json = await SendRequestAsync($"{Endpoints.BASE_URL}{Endpoints.LOCAL}");

            var deserializedResponse = JsonConvert.DeserializeObject<List<LocalResponseEntity>>(json);

            return deserializedResponse.FirstOrDefault();
        }

        /// <summary>
        /// Gets the COVID statistics for all available countries asynchronously.
        /// </summary>
        /// <returns>A <see cref="List{T}" /> of <see cref="InternationalResponseEntityData" /> objects containing the statistics of each country.</returns>
        public async Task<List<InternationalResponseEntityData>> GetAllCountriesDataAsync()
        {
            string json = await SendRequestAsync($"{Endpoints.BASE_URL}");

            var deserializedResponse = JsonConvert.DeserializeObject<List<InternationalResponseEntity>>(json);

            // Sort them into a new object, just to make it neat.
            List<InternationalResponseEntityData> internationalResponseEntityDatas = new List<InternationalResponseEntityData>();

            foreach (var country in deserializedResponse)
            {
                InternationalResponseEntityData countryData = new InternationalResponseEntityData()
                {
                    ObjectId = country.Attributes.ObjectId,
                    Country = country.Attributes.Country,
                    // I have no idea why the given unix timestamp has 3 additional zeros at the end.
                    LastUpdated = Convert.ToInt64(country.Attributes.LastUpdated.ToString().Remove(10)),
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
        /// <param name="searchMode"> The search mode to use.</param>
        /// <returns>A <see cref="InternationalResponseEntityData" /> object containing the statistic of the given country name.</returns>
        public async Task<InternationalResponseEntityData> GetCountryDataAsync(string countryName, SearchMode searchMode)
        {
            string json = await SendRequestAsync($"{Endpoints.BASE_URL}");

            var deserializedResponse = JsonConvert.DeserializeObject<List<InternationalResponseEntity>>(json);

            if (!deserializedResponse.Any(x => x.Attributes.Country.ToLowerInvariant() == countryName.ToLowerInvariant()))
            {
                if (searchMode is SearchMode.ClosestMatching)
                {
                    if (!deserializedResponse.Any(x => x.Attributes.Country.ToLowerInvariant().Contains(countryName.ToLowerInvariant())))
                    {
                        throw new ArgumentException($"Closest matching name of country is not found in JSON response.", nameof(countryName));
                    }

                    else
                    {
                        deserializedResponse.RemoveAll(x => !x.Attributes.Country.ToLowerInvariant().Contains(countryName.ToLowerInvariant()));
                    }
                }

                else
                {
                    throw new ArgumentException($"Name of country is not found in JSON response.", nameof(countryName));
                }
            }

            else
            {
                deserializedResponse.RemoveAll(x => x.Attributes.Country.ToLowerInvariant() != (countryName.ToLowerInvariant()));
            }

            InternationalResponseEntityData internationalResponseEntity = new InternationalResponseEntityData()
            {
                ObjectId = deserializedResponse.FirstOrDefault().Attributes.ObjectId,
                Country = deserializedResponse.FirstOrDefault().Attributes.Country,
                // I have no idea why the given unix timestamp has 3 additional zeros at the end.
                LastUpdated = Convert.ToInt64(deserializedResponse.FirstOrDefault().Attributes.LastUpdated.ToString().Remove(10)),
                Latitude = deserializedResponse.FirstOrDefault().Attributes.Latitude,
                Longitude = deserializedResponse.FirstOrDefault().Attributes.Longitude,
                Confirmed = deserializedResponse.FirstOrDefault().Attributes.Confirmed,
                Deaths = deserializedResponse.FirstOrDefault().Attributes.Deaths,
                Recovered = deserializedResponse.FirstOrDefault().Attributes.Recovered,
                Active = deserializedResponse.FirstOrDefault().Attributes.Active
            };

            return internationalResponseEntity;
        }

        /// <summary>
        /// Gets the numbers for either global positives, recovered or deaths.
        /// </summary>
        /// <param name="dataType">The <see cref="DataType" /> to get.</param>
        /// <returns>A <see cref="PartialResponseEntity" /> object containing the name and numbers of the requested <see cref="DataType" />.</returns>
        public async Task<PartialResponseEntity> GetPartialGlobalDataAsync(DataType dataType)
        {
            string json = string.Empty;

            if (dataType is DataType.Positive)
            {
                json = await SendRequestAsync($"{Endpoints.BASE_URL}{Endpoints.GLOBAL_POSITIVE}");
            }

            else if (dataType is DataType.Recovered)
            {
                json = await SendRequestAsync($"{Endpoints.BASE_URL}{Endpoints.GLOBAL_RECOVERED}");
            }

            else if (dataType is DataType.Deaths)
            {
                json = await SendRequestAsync($"{Endpoints.BASE_URL}{Endpoints.GLOBAL_DEATHS}");
            }

            return JsonConvert.DeserializeObject<PartialResponseEntity>(json);
        }

        /// <summary>
        /// Sends a request to the endpoint.
        /// </summary>
        /// <param name="endpoint">The desired endpoint URL.</param>
        /// <returns>A JSON string.</returns>
        internal async Task<string> SendRequestAsync(string endpoint)
        {
            string json = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            return json;
        }
    }
}
