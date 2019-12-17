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
    public partial class MyProfile : ContentPage
    {
        public MyProfile()
        {
            InitializeComponent();
            GetProfile(Settings.Id);
        }

        public async void GetProfile(string id)
        {
            ApiService apiService = new ApiService();
            var donor = await apiService.GetDonor(id);
            var bloodType = await apiService.GetBloodType(id);
            var organType = await apiService.GetOrganType(id);

            ImgProfile.Source = donor.FullLogoPath;
            LblName.Text = donor.Name;
            LblEmail.Text = donor.Email;
            LblGender.Text = donor.Gender;
            LblPhone.Text = donor.Phone;
            LblRegion.Text = donor.Region;
            LblLocation.Text = donor.Location;
            LblBlood.Text = bloodType.BloodType;
            Lstvw.ItemsSource = organType;
            Overlay.IsVisible = false;

        }
    }
}