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
	public partial class NewItemSavePage : ContentPage
	{
        NewItemSaveViewModel viewModel;

        public Item Item { get; set; }
        public NewItemSavePage(NewItemSaveViewModel viewModel)
		{
			InitializeComponent ();
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
            viewModel.Item.WatchAction = viewModel.MarketActionsSelectedIndex;
            MessagingCenter.Send(this, "AddItem", viewModel.Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


    }
}