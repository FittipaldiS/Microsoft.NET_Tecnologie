using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReportExcelFileMST
{
    [TestClass]
    public class EndPointFootbalAccess
    {
        [TestMethod]
        public async Task EndPointFootbalAccessTestAsync()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/leagues"),
                Headers =
                {
                   { "X-RapidAPI-Key", "" },
                   { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                },
            };


            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }

        }
    }
}
