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
    public partial class NewItemSearchPage : ContentPage
    {
        NewItemSearchViewModel viewModel;

        public NewItemSearchPage(string crossDbUri = null)
        {
            InitializeComponent();

            var item = new CrossDbItemTypes
            {
                Id = "Item 1",
                Name = "This is an item description."
            };

            viewModel = new NewItemSearchViewModel();
            BindingContext = viewModel;

            viewModel.LoadCrossDbItemCommand.Execute(crossDbUri);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

            var crossDbItem = args.SelectedItem as CrossDbItemTypes;
            if (crossDbItem == null)
                return;
            var item = new Item
            {
                Id = Guid.NewGuid().ToString(),
                CrossItemId = crossDbItem.Id,
                Name = crossDbItem.Name,
                TypeName = crossDbItem.TypeName,
                Image = crossDbItem.Image,
                LastUpdateTime = crossDbItem.LastUpdateTime,
                FormatSellPrice = crossDbItem.FormatSellPrice,
                FormatBuyPrice = crossDbItem.FormatBuyPrice,
            };
            await Navigation.PushAsync(new NewItemSavePage(new NewItemSaveViewModel(item)));

            // Manually deselect item.
            DbItemsListView.SelectedItem = null;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}