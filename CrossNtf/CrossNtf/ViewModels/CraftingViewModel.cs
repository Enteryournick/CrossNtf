using CrossNtf.Models;
using CrossNtf.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossNtf.ViewModels
{
    class CraftingViewModel : BaseViewModel
    {

        //Choose category strings
        string chooseCategory = string.Empty;
        public string ChooseCategory
        {
            get { return chooseCategory; }
            set { SetProperty(ref chooseCategory, value); }
        }

        List<CrossDbItemTypes> categories;
        public List<CrossDbItemTypes> Categories
        {
            get { return categories; }
            set { SetProperty(ref categories, value); }
        }

        bool isCategoriesEnabled = true;

        public bool IsCategoriesEnabled
        {
            get { return isCategoriesEnabled; }
            set { SetProperty(ref isCategoriesEnabled, value); }
        }

        //Choose faction strings
        string chooseFaction = string.Empty;
        public string ChooseFaction
        {
            get { return chooseFaction; }
            set { SetProperty(ref chooseFaction, value); }
        }

        List<CrossDbItemTypes> factions;
        public List<CrossDbItemTypes> Factions
        {
            get { return factions; }
            set
            {
                SetProperty(ref factions, value);
                OnPropertyChanged("FactionsSelectedIndex");
            }
        }

        bool isFactionsEnabled = true;
        public bool IsFactionsEnabled
        {
            get { return isFactionsEnabled; }
            set { SetProperty(ref isFactionsEnabled, value); }
        }

        //Choose type strings
        string chooseType = string.Empty;
        public string ChooseType
        {
            get { return chooseType; }
            set { SetProperty(ref chooseType, value); }
        }

        List<CrossDbItemTypes> types;

        public List<CrossDbItemTypes> Types
        {
            get { return types; }
            set { SetProperty(ref types, value); }
        }

        bool isTypesEnabled = true;

        public bool IsTypesEnabled
        {
            get { return isTypesEnabled; }
            set { SetProperty(ref isTypesEnabled, value); }
        }

        //Choose rarity strings
        string chooseRarity = string.Empty;
        public string ChooseRarity
        {
            get { return chooseRarity; }
            set { SetProperty(ref chooseRarity, value); }
        }

        //result items
        List<CrossDbItemTypes> items;

        public List<CrossDbItemTypes> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        bool isItemsEnabled = false;

        public bool IsItemsEnabled
        {
            get { return isItemsEnabled; }
            set { SetProperty(ref isItemsEnabled, value); }
        }

        public Command LoadCrossDbItemCommand { get; set; }

        public ICrossDb Cdb => DependencyService.Get<ICrossDb>();

        int factionsSelectedIndex;

        public int FactionsSelectedIndex
        {
            get { return factionsSelectedIndex; }
            set { SetProperty(ref factionsSelectedIndex, value);}
        }

        public CraftingViewModel()
        {
            Title = Translater.ProvideValue("Crafting search page");

            ChooseCategory = Translater.ProvideValue("Choose category");
            ChooseFaction = Translater.ProvideValue("Choose faction");
            ChooseType = Translater.ProvideValue("Choose type");
            ChooseRarity = Translater.ProvideValue("Choose rarity");

            Factions = new List<CrossDbItemTypes>();
            //Categories = new List<CrossDbItemTypes>();

            LoadCrossDbItemCommand = new Command<string>(async (requestedItems) => await ExecuteLoadCrossDbItemCommand(requestedItems));

            //FactionsSelectedIndex = 0;


        }

        async Task ExecuteLoadCrossDbItemCommand(string requestedItems)
        {
            string[] uriParts = requestedItems.Split('?');
            //PropertyInfo itemsPickerEnabled = (PropertyInfo) GetType().GetRuntimeProperty(string.Format("Is{0}Enabled", uriParts[0]));
            PropertyInfo itemsPickerEnabled = (PropertyInfo)GetType().GetRuntimeProperty("IsItemsEnabled");
            PropertyInfo itemsNode = (PropertyInfo)GetType().GetRuntimeProperty(uriParts[0]);
            var crossDbItems = new List<CrossDbItemTypes>();// = (List<CrossDbItemTypes>)GetType().GetRuntimeProperty(requestedItems).GetValue(this);

            itemsPickerEnabled.SetValue(this, true);

            try
            {
                crossDbItems.Clear();
                var items = await Cdb.GetCrossDbItem(requestedItems);
                foreach (var item in items)
                {
                    crossDbItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                itemsNode.SetValue(this, crossDbItems);
                itemsPickerEnabled.SetValue(this, false);
            }
        }

    }
}
