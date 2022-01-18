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
    }
}
