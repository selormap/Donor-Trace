using DonorTraceMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {        
            Overlay.IsVisible = true;
            ApiService apiServices = new ApiService();
            bool response = await apiServices.Login(EntEmail.Text, EntPassword.Text);
            if (!response)
            {              
                Overlay.IsVisible = false;
                await DisplayAlert("Alert", "Login failed", "Cancel");
            }
            else
            {            
               Overlay.IsVisible = false;
                if (Settings.Role == "Administrator")
                {
                    Application.Current.MainPage = new AdminMaster();
                }
                else if (Settings.Role == "Medical Officer")
                {
                    Application.Current.MainPage = new FacilityMaster();
                }
                else
                {
                    Application.Current.MainPage = new MasterPage();
                }        
            }
        }
        private void TapSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}