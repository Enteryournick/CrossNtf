using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CrossNtf.Models;
using CrossNtf.Views;
using Plugin.LocalNotifications;
using CrossNtf.Services;
using System.Linq;

namespace CrossNtf.ViewModels
{
    class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command GetCrossDbLastUpdateTimeCommand { get; set; }
        public Command StartCheckWatchesTimerCommand { get; set; }
        public WatchService Wservice = new WatchService();
        public ICrossDb Cdb => DependencyService.Get<ICrossDb>();
        public bool DoCheckWatches { get; set; } = false;

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            GetCrossDbLastUpdateTimeCommand = new Command(async () => await GetCrossDbLastUpdateTime());
            StartCheckWatchesTimerCommand = new Command<string>(async (lastRunTime) => await StartCheckWatchesTimer(lastRunTime));

            MessagingCenter.Subscribe<NewItemSavePage, Item>(this, "AddItem", async (obj, item) =>
            {
                //if (!DoCheckWatches)
                 //   DoCheckWatches = true;
                var newItem = item as Item;
                Items.Add(newItem);
                if(!DoCheckWatches)
                    StartCheckWatchesTimerCommand.Execute(Wservice.LastUpdateTime);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "UpdateItem", async (obj, item) =>
            {
                var newItem = item as Item;
                var oldItem = Items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
                Items.Remove(oldItem);
                Items.Add(newItem);
                
                await DataStore.UpdateItemAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                var newItem = item as Item;
                var oldItem = Items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
                Items.Remove(oldItem);

                if (Items.Count() == 0)
                    DoCheckWatches = false;

                await DataStore.DeleteItemAsync(newItem.Id);
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetCrossDbLastUpdateTime()
        {
            Wservice.LastUpdateTime = await Cdb.GetCrossDbLastUpdateTime();
        }

        public async Task StartCheckWatchesTimer(string lastRunTime)
        {
            //CrossLocalNotifications.Current.Show("title", lastRunTime);

            DoCheckWatches = true;

            DateTime lastRunTimeDateTime = DateTime.Parse(lastRunTime);
            //lastRunTimeDateTime = lastRunTimeDateTime.AddSeconds(310);
            int delay = Convert.ToInt32(lastRunTimeDateTime.AddSeconds(325).Subtract(DateTime.Now).TotalMilliseconds);
            
            
            if (delay > 0)
                await Task.Delay(delay);

            while (DoCheckWatches)
            {

                string notificationString = await Wservice.CheckWatches(Items);
                // Update the UI (because of async/await magic, this is still in the UI thread!)
                if(notificationString != null)
                    CrossLocalNotifications.Current.Show(Translater.ProvideValue("Overbid:"), notificationString);

                if (DoCheckWatches)
                {
                    await Task.Delay(TimeSpan.FromSeconds(310));
                }
            }

            /*Device.StartTimer(TimeSpan.FromSeconds(10), () =>
             {
                 Task.Factory.StartNew(async () =>
                 {
                     // Do the actual request and wait for it to finish.

                     if (Wservice.LastUpdateTime != null)
                         await CheckWatches();
                     // Switch back to the UI thread to update the UI
                     Device.BeginInvokeOnMainThread(() =>
                     {
                         // Update the UI
                         // ...
                         // Now repeat by scheduling a new request
                         //ScheduleWebRequest();
                     });
                 });

                 // Don't repeat the timer (we will start a new timer when the request is finished)
                 return false;
             });*/
        }
    }
}