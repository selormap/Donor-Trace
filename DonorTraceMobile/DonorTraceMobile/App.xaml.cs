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

            if (!string.IsNullOrEmpty(Settings.Token))
            {
                Current.MainPage = new MasterPage();

            }
            else if (string.IsNullOrEmpty(Settings.Email) && string.IsNullOrEmpty(Settings.Password))
            {
                Current.MainPage = new LoginPage();
                // MainPage = new RegisterOrganPage();
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
