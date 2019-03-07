using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CrossNtf.Models;
using Plugin.Multilingual;
using CrossNtf.ViewModels;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CrossNtf.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /*public class CrossDbRequestParams
    {
        public string CrossDbUri = "items";
        public ObservableCollection<CrossDbItemTypes> CrossDbItems { get; set; }
        public PropertyInfo IsEnabled;
    }*/

    public partial class NewItemPage : ContentPage
    {
        public CrossDbItemTypes Item { get; set; }
      //  public CrossDbRequestParams crossDbRequestParams;

        NewItemViewModel viewModel;

        public NewItemPage()
        {
            InitializeComponent();

            Item = new CrossDbItemTypes
            {
                Id = "Item name",
                Name = "This is an item description."
            };

            BindingContext = viewModel = new NewItemViewModel();
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

            viewModel.LoadCrossDbItemCommand.Execute("Factions");

            /*crossDbRequestParams = new CrossDbRequestParams
            {
                CrossDbUri = "Categories",
                CrossDbItems = viewModel.Categories
            };

            crossDbRequestParams.IsEnabled = viewModel.GetType().GetRuntimeProperty("IsCategoriesBusy");*/

            viewModel.LoadCrossDbItemCommand.Execute("Categories");
            viewModel.LoadCrossDbItemCommand.Execute("Types");
            //fPicker.Items.Add("Capuchin Monkey");
        }

        

        async void Search_Clicked(object sender, EventArgs args)
        {
            //CrossDbItemTypes rselected = (CrossDbItemTypes) rpicker.SelectedItem;
            string categorySelUri = "category=" + ((CrossDbItemTypes) cPicker.SelectedItem)?.Name.ToLower();
            string factionSelUri  = "&faction="  + ((CrossDbItemTypes) fPicker.SelectedItem)?.Name.ToLower();
            string raritySelUri   = "&rarity="   + ((string) rPicker.SelectedItem)?.ToLower();
            string searchQueryUri   = "&query="   + searchQuery.Text;
            //viewModel.LoadCrossDbItemCommand.Execute("Items?" + categorySelUri + factionSelUri + raritySelUri);
            await Navigation.PushAsync(new NewItemSearchPage("Items?" + categorySelUri + factionSelUri + raritySelUri + searchQueryUri));
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