using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CrossNtf.Models;
using Newtonsoft.Json;

namespace CrossNtf.Services
{
    class CrossDb : ICrossDb
    {
        HttpClient client;

        public List<CrossDbItemTypes> Factions { get; set; }

        public static string CrossDbUrl = "https://crossoutdb.com/api/v1/{0}";

        public CrossDb()
        {
            //var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<List<CrossDbItemTypes>> GetCrossDbItem(String UriCrossDbParam)
        {
            Factions = new List<CrossDbItemTypes>();

            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(CrossDbUrl, UriCrossDbParam));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Factions = JsonConvert.DeserializeObject<List<CrossDbItemTypes>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Factions;
        }

        public async Task<String> GetCrossDbLastUpdateTime()
        {
            //Factions = new List<CrossDbItemTypes>();

            //RestUrl = "https://crossoutdb.com//api/v1/item/1";
            var uri = new Uri(string.Format(CrossDbUrl, "item/1"));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Factions = JsonConvert.DeserializeObject<List<CrossDbItemTypes>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Factions[0].LastUpdateTime;
        }
    }
}
