using DonorTraceMobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            ApiService apiService = new ApiService();
            HttpResponseMessage response = await apiService.RegisterUser(EntEmail.Text, EntPassword.Text, confirmPassword.Text);
            if (!response.IsSuccessStatusCode)
            {
                
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            else
            {
                await DisplayAlert("Success", "Your account has been created", "Ok");
                await Navigation.PopToRootAsync();
            }
        }

        private void ConfirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(confirmPassword.Text))
            {
                pass._password = EntPassword.Text;
            }
        }
    }
}