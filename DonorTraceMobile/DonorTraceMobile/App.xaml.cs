using System;
using Xamarin.Forms;
using DonorTraceMobile.Pages;
using Xamarin.Forms.Xaml;


namespace DonorTraceMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // MainPage = new NavigationPage(new MapPage());

            if (!string.IsNullOrEmpty(Settings.Token))
            {
                if (Settings.Role == "Administrator")
                {
                    Current.MainPage = new AdminMaster();
                }

                else if (Settings.Role == "Medical Officer")
                {
                    Current.MainPage = new FacilityMaster();
                }
                else
                    Current.MainPage = new MasterPage();

            }
            else if (string.IsNullOrEmpty(Settings.Email) && string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());

            }
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
