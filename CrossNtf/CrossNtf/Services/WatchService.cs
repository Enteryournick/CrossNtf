using CrossNtf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossNtf.Services
{
    public class WatchService
    {
        public String LastUpdateTime = null;

        public static string CrossDbUrl = "https://crossoutdb.com/api/v1/{0}";

        public WatchService()
        {
            //var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            // await GetCrossDbLastUpdateTime();
            //client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            Task.Run(async () => await GetCrossDbLastUpdateTime()).Wait();
        }


        private async Task GetCrossDbLastUpdateTime()
        {
            string LastUpdateTime = await DependencyService.Get<ICrossDb>().GetCrossDbLastUpdateTime();
        }

        public async Task<string> CheckWatches(ObservableCollection<Item> items)
        {
            string notificationString = null;

            foreach (var item in items)
            {
                var checkItem = await DependencyService.Get<ICrossDb>().GetCrossDbItem("item/"+ item.CrossItemId);
                switch (item.WatchAction)
                {
                    case "Sell":
                        if (checkItem[0].FormatSellPrice < item.WatchPrice)
                            notificationString += item.Name + "\n";
                        break;
                    case "Buy":
                        if (checkItem[0].FormatBuyPrice > item.WatchPrice)
                            notificationString += item.Name + "\n";
                        break;
                }
            }
        

            return notificationString;
        }
    }
}
