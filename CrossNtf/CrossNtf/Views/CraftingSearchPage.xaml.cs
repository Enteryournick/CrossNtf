using CrossNtf.Models;
using CrossNtf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossNtf.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CraftingSearchPage : ContentPage
	{
        CraftingSearchViewModel viewModel;

        public CraftingSearchPage (string crossDbUri = null)
        {
            InitializeComponent();

            var item = new CrossDbItemTypes
            {
                Id = "Item 1",
                Name = "This is an item description."
            };

            viewModel = new CraftingSearchViewModel();
            BindingContext = viewModel;

            viewModel.LoadCrossDbItemCommand.Execute(crossDbUri);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

           /* var crossDbItem = args.SelectedItem as CrossDbItemTypes;
            if (crossDbItem == null)
                return;
            var item = new Item
            {
                Id = crossDbItem.Id,
                Name = crossDbItem.Name,
                Description = crossDbItem.Description,
                LastUpdateTime = crossDbItem.LastUpdateTime,
                FormatSellPrice = crossDbItem.FormatSellPrice,
                FormatBuyPrice = crossDbItem.FormatBuyPrice,
            };
            await Navigation.PushAsync(new NewItemSavePage(new NewItemSaveViewModel(item)));

            // Manually deselect item.
            DbItemsListView.SelectedItem = null;*/
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}