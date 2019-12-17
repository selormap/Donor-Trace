using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void BtnChange_OnClicked(object sender, EventArgs e)
        {
            Overlay.IsVisible = true;
            ApiService apiService = new ApiService();

            bool response = await apiService.ChangePassword(TxtOldPass.Text, TxtNewPass.Text, Settings.Id);

            if (!response)

            {
                Overlay.IsVisible = false;
                await DisplayAlert("Oops", "Something wrong", "Cancel");

            }

            else

            {
                Overlay.IsVisible = false;
                await DisplayAlert("Password Changed", "Kindly login with the new password", "Alright");

                Settings.Email = string.Empty;
                Settings.Password = string.Empty;
                Settings.Token = string.Empty;

                Application.Current.MainPage = new NavigationPage(new LoginPage());

            }
        }
    }
}