using System;
using System.Collections.Generic;
using CrossNtf.Models;

namespace CrossNtf.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public string ChooseAction { get; set; }

        public ItemDetailViewModel(Item item = null)
        {
            Title = Translater.ProvideValue("Update watch");
            
            Item = new Item
            {
                Id = item.Id,
                Name = item.Name,
                TypeName = item.TypeName,
                Image = item.Image,
                LastUpdateTime = item.LastUpdateTime,
                FormatSellPrice = item.FormatSellPrice,
                FormatBuyPrice = item.FormatBuyPrice,
                WatchAction = item.WatchAction,
                WatchPrice = item.WatchPrice
            };
            //Item = item;
            ChooseAction = "Choose action";
            // MarketActionsSelectedIndex = 0;
            // Item.WatchPrice = Item.FormatSellPrice;

        }
    }
}
