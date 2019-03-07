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
	public partial class CraftingPage : ContentPage
	{
        public CrossDbItemTypes Item { get; set; }
        //  public CrossDbRequestParams crossDbRequestParams;

        CraftingViewModel viewModel;

        public CraftingPage()
        {
            InitializeComponent();

            Item = new CrossDbItemTypes
            {
                Id = "Item name",
                Name = "This is an item description."
            };

            BindingContext = viewModel = new CraftingViewModel();
          //  viewModel.FactionsSelectedIndex = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*crossDbRequestParams = new CrossDbRequestParams
            {
                CrossDbUri = "Factions",
                CrossDbItems = viewModel.Factions
            };

            crossDbRequestParams.IsEnabled = viewModel.GetType().GetRuntimeProperty("IsFactionsBusy");*/

            if(viewModel.Factions.Count == 0) viewModel.LoadCrossDbItemCommand.Execute("Factions");

            /*crossDbRequestParams = new CrossDbRequestParams
            {
                CrossDbUri = "Categories",
                CrossDbItems = viewModel.Categories
            };

            crossDbRequestParams.IsEnabled = viewModel.GetType().GetRuntimeProperty("IsCategoriesBusy");*/

            //viewModel.LoadCrossDbItemCommand.Execute("Categories");
            //viewModel.LoadCrossDbItemCommand.Execute("Types");
             // viewModel.FactionsSelectedIndex = 0;
            //fPicker.Items.Add("Capuchin Monkey");
            //fPicker.SelectedIndex = 0;
        }



        async void Search_Clicked(object sender, EventArgs args)
        {
            //CrossDbItemTypes rselected = (CrossDbItemTypes) rpicker.SelectedItem;
            string categorySelUri = "category="; //+ ((CrossDbItemTypes)cPicker.SelectedItem)?.Name.ToLower();
            string factionSelUri = "&faction=" + ((CrossDbItemTypes)fPicker.SelectedItem)?.Name.ToLower();
            string raritySelUri = "&rarity=" + ((string)rPicker.SelectedItem)?.ToLower();
            //viewModel.LoadCrossDbItemCommand.Execute("Items?" + categorySelUri + factionSelUri + raritySelUri);
            await Navigation.PushAsync(new CraftingSearchPage("Items?" + categorySelUri + factionSelUri + raritySelUri));
            //CrossDbItemTypes cselected = (CrossDbItemTypes) cPicker.SelectedItem;
            //CrossDbItemTypes fselected = (CrossDbItemTypes) fPicker.SelectedItem;
            //viewModel.LoadCrossDbItemCommand.Execute("Items" + string.Format("?rarity={0}&category={1}&faction{2}=&removedItems=false&metaItems=false&query={3}", rPicker.SelectedItem, cselected.Name, fselected.Name, squery.Text).ToLower());
            //viewModel.LoadCrossDbItemCommand.Execute(string.Format("items?rarity{0}=&category{1}=&faction{2}=&removedItems=false&metaItems=false&query={3}", rPicker.SelectedItem, cPicker.SelectedItem, fPicker.SelectedItem, squery.Text));
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}