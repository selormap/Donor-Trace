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
    public partial class AddOfficer : ContentPage
    {
        readonly ApiService _apiServices = new ApiService();
        public AddOfficer()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
           

            EntFacility.ItemsSource = await _apiServices.GetFacilities();
           
            base.OnAppearing();

        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {
            Overlay.IsVisible = true;

            var facilityId = ((FacilityList) EntFacility.SelectedItem).Id;

            var officer = new OfficerModel()
            {
                UserName = EntUserName.Text,
                Department = EntDept.Text,
                FacilityId = facilityId,
                Firstname = EntFirst.Text,
                Lastname = EntLast.Text,
                ContactNo = EntContactNo.Text,
                CreatedBy = Settings.Email

            };

            var response = await _apiServices.AddOfficer(officer);

            if (!response)
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            else
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Success", "Officer added successfully", "Ok");
                await _apiServices.FacilityUserSms(officer);


                EntUserName.Text = "";
                EntLast.Text = "";
                EntContactNo.Text = "";
                EntFirst.Text = "";
                EntDept.Text = "";
                EntFacility.Items.Clear();
                await Navigation.PushAsync(new FacilityUsers());
            }
        }
    }
}