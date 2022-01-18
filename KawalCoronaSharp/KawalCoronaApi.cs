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
        /// Gets the international COVID statistics asynchronously.
        /// </summary>
        /// <returns>A <see cref="List{T}" /> of <see cref="InternationalResponseEntity" /> objects containing the statistics of each country.</returns>
        public async Task<List<InternationalResponseEntity>> GetInternationalDataAsync()
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

            return JsonConvert.DeserializeObject<List<InternationalResponseEntity>>(json);
        }
    }
}
