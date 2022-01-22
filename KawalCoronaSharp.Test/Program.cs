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

            Console.WriteLine($"LOCAL RESPONSE:\nCountry: {response.Country}\nPositive: {response.Positives}\nRecovered: {response.Recovered}\nDeceased: {response.Deceased}\nHospitalized: {response.Hospitalised}");

            var countryResponse = await api.GetCountryDataAsync("indo", SearchMode.ClosestMatching);

            Console.WriteLine($"\nData for {countryResponse.Country} (GetCountryDataAsync)\nLast updated: {new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(countryResponse.LastUpdated)}\nConfirmed: {countryResponse.Confirmed}\nActive: {countryResponse.Active}\nRecovered: {countryResponse.Recovered}\nDeaths: {countryResponse.Deaths}\nLatitude: {countryResponse.Latitude}\nLongitude: {countryResponse.Longitude}\nObject ID: {countryResponse.ObjectId}\n");

            var intResponse = await api.GetAllCountriesDataAsync();

            foreach (var data in intResponse)
            {
                Console.WriteLine($"\nData for {data.Country} (GetAllCountriesDataAsync)\nLast updated: {new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(data.LastUpdated)}\nConfirmed: {data.Confirmed}\nActive: {data.Active}\nRecovered: {data.Recovered}\nDeaths: {countryResponse.Deaths}\nLatitude: {data.Latitude}\nLongitude: {data.Longitude}\nObject ID: {data.ObjectId}\n");
            }

            var globalPositive = await api.GetPartialGlobalDataAsync(DataType.Positive);
            var globalRecovered = await api.GetPartialGlobalDataAsync(DataType.Recovered);
            var globalDeaths = await api.GetPartialGlobalDataAsync(DataType.Deaths);

            Console.WriteLine($"Global positives: {globalPositive.Value}\nGlobal recovered: {globalRecovered.Value}\nGlobal deaths: {globalDeaths.Value}");

            Console.ReadLine();
        }
    }
}
