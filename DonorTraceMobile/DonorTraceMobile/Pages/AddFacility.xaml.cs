using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceMobile.Models;
using DonorTraceMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFacility : ContentPage
    {
        ApiService apiServices = new ApiService();
        public AddFacility()
        {
            InitializeComponent();
        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {
            Overlay.IsVisible = true;

            var facility = new FacilityModel()
            {
                Name = EntName.Text,
                RegistrationNo = EntRegNumber.Text,
                ContactNo = EntContactNo.Text,
                Address = EntAddress.Text,
                CreatedBy = Settings.Email
            };

            var response = await apiServices.AddFacility(facility);

            if (!response)
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            else
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Success", "Facility added successfully", "Ok");

                EntRegNumber.Text = "";
                EntAddress.Text = "";
                EntContactNo.Text = "";
                EntName.Text = "";
            }
        }
    }
}