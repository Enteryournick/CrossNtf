using System;
using System.Collections.Generic;
using System.Text;

namespace CrossNtf.Models
{
    public enum MenuItemType
    {
        Browse,
        Crafting,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
