using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceMobile.Services;
using Plugin.Share;
using Plugin.Share.Abstractions;
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

        private void btnShare_Clicked(object sender, EventArgs e)
        {
            CrossShare.Current.Share(new ShareMessage
            {
                Text = "Become a volunteer on the Donor Trace App",
                Title = "Donor Trace App",
                Url = "http://.donortrace.com"
            },
           new ShareOptions
           {
               ChooserTitle = "Donor Trace App",
               ExcludedUIActivityTypes = new[] { ShareUIActivityType.PostToFacebook, ShareUIActivityType.AirDrop,
                ShareUIActivityType.PostToFlickr, ShareUIActivityType.PostToVimeo, ShareUIActivityType.Print}
           });
        }
    }
}