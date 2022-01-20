using System;
using System.Threading.Tasks;
using KawalCoronaSharp.Entities;
using KawalCoronaSharp.Enums;

namespace KawalCoronaSharp.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            KawalCoronaApi api = new KawalCoronaApi();

            var response = await api.GetIndonesianDataAsync();

            Console.WriteLine($"LOCAL RESPONSE:\nCountry: {response.Country}\nPositive: {response.Positives}\nHealed: {response.Recovered}\nDeceased: {response.Deceased}\nHospitalized: {response.Hospitalised}");

            var localResponse = await api.GetCountryDataAsync("Indonesia");

            Console.WriteLine($"\n\nDeaths: {localResponse.Deaths}");

            var intResponse = await api.GetGlobalDataAsync();

            foreach (var data in intResponse)
            {
                Console.WriteLine(data.LastUpdated);
            }

            var globalResp = await api.GetPartialResponseDataAsync(DataType.Positive);

            Console.WriteLine(globalResp.Value);

            Console.ReadLine();
        }
    }
}
