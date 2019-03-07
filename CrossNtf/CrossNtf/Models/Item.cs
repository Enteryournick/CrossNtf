using System;

namespace CrossNtf.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string CrossItemId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string TypeName { get; set; }
        public string LastUpdateTime { get; set; }
        public string WatchAction { get; set; }
        public double WatchPrice { get; set; }
        public double FormatSellPrice { get; set; }
        public double FormatBuyPrice { get; set; }
    }
}