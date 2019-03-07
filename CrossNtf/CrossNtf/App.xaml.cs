using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CrossNtf.Services;
using CrossNtf.Views;
using Plugin.Multilingual;
using CrossNtf.Resources;
using System.Globalization;
using System.Threading.Tasks;
using Plugin.LocalNotifications;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CrossNtf
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        //public static string CrossDbUrl = "https://crossoutdb.com//api/v1/{0}";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();
            //CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("en");
            //CrossNtfLang.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            CrossNtfLang.Culture = CrossMultilingual.Current.DeviceCultureInfo;

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            DependencyService.Register<CrossDb>();
            DependencyService.Register<WatchService>();
            
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
