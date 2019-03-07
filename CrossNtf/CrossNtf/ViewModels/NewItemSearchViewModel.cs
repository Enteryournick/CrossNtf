using CrossNtf.Models;
using CrossNtf.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CrossNtf.Views;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CrossNtf.ViewModels
{
    class NewItemSearchViewModel : BaseViewModel
    {

        
        //result items
        public ObservableCollection<CrossDbItemTypes> Items { get; set; }

        bool isSearchPageRefreshing = false;

        public bool IsSearchPageRefreshing
        {
            get { return isSearchPageRefreshing; }
            set { SetProperty(ref isSearchPageRefreshing, value); }
        }

        public Command LoadCrossDbItemCommand { get; set; }

        public ICrossDb Cdb => DependencyService.Get<ICrossDb>();

        public NewItemSearchViewModel()
        {
            Title = Translater.ProvideValue("Add watch");

            Items = new ObservableCollection<CrossDbItemTypes>();

            LoadCrossDbItemCommand = new Command<string>(async (requestedItems) => await ExecuteLoadCrossDbItemCommand(requestedItems));


        }

        async Task ExecuteLoadCrossDbItemCommand(string requestedItems)
        {
            string[] uriParts = requestedItems.Split('?');

            IsSearchPageRefreshing = true;

            try
            {
                Items.Clear();
                var items = await Cdb.GetCrossDbItem(requestedItems);
                foreach (var item in items)
                {
                    item.Image = "https://crossoutdb.com/img/items/" + item.Image;
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                
                IsSearchPageRefreshing = false;
            }
        }

    }
}
