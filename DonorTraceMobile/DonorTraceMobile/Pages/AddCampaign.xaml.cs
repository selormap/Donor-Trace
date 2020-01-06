using DonorTraceMobile.Models;
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
    public partial class AddCampaign : ContentPage
    {
        ApiService apiServices = new ApiService();
        public AddCampaign()
        {
            InitializeComponent();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            Overlay.IsVisible = true;

            var campaign = new CampaignModel()
            {
                Organization = EntName.Text,
                Description = EntDesc.Text,
                Location = EntLocation.Text,
                EventDate = EntDate.Date + EntTime.Time,
                
            };

            var response = await apiServices.AddCampaign(campaign);

            if (!response)
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            else
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Success", "Campaign added successfully", "Ok");

                EntName.Text = "";
                EntLocation.Text = "";
                EntDesc.Text = "";
                await apiServices.SendSms(campaign);
                await Navigation.PushAsync(new Campaigns());
               
               
               

            }
        }
    }
}