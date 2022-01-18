using System;
using System.Threading.Tasks;

namespace KawalCoronaSharp.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            KawalCoronaApi api = new KawalCoronaApi();

            var response = await api.GetIndonesianDataAsync();

            Console.WriteLine($"LOCAL RESPONSE:\nCountry: {response.Country}\nPositive: {response.Positives}\nHealed: {response.Healed}\nDeceased: {response.Deceased}\nHospitalized: {response.Hospitalised}");

            var intResponse = await api.GetInternationalDataAsync();

            foreach (var data in intResponse)
            {
                Console.WriteLine(data.Attributes.Country);
            }

            Console.ReadLine();
        }
    }
}
