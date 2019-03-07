using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CrossNtf.Models;
using CrossNtf.ViewModels;

namespace CrossNtf.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public Item Item { get; set; }
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        /*public NewItemSavePage()
        {
            InitializeComponent();

            var item = new CrossDbItemTypes
            {
                Id = "Item 1",
                Name = "This is an item description."
            };

            viewModel = new NewItemSaveViewModel(item);
            BindingContext = viewModel;
        }*/

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
            await Navigation.PopModalAsync();
        }
    }
}