using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CrossNtf.Models;
using CrossNtf.Views;
using Xamarin.Forms;

namespace CrossNtf.ViewModels
{
    public class NewItemSaveViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public List<String> MarketActions { get; set; }
        public string ChooseAction { get; set; }

        string marketActionsSelectedIndex = "Sell";
        
        public string MarketActionsSelectedIndex
        {
            get { return marketActionsSelectedIndex; }
            set
            {
                switch (value)
                {
                    case "Sell":
                        Item.WatchPrice = Item.FormatSellPrice;
                        break;
                    case "Buy":
                        Item.WatchPrice = Item.FormatBuyPrice;
                        break;

                }
                OnPropertyChanged("Item");
                SetProperty(ref marketActionsSelectedIndex, value);
            }
        }
        public NewItemSaveViewModel(Item item = null)
        {
            Title = Translater.ProvideValue("Save watch");
            MarketActions = new List<String>
            {
                "Sell",
                "Buy"
            };
            Item = item;
            ChooseAction = "Choose action";
           // MarketActionsSelectedIndex = 0;
           // Item.WatchPrice = Item.FormatSellPrice;

        }
    }
}
