using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonorProfile : ContentPage
    {
        private string _number;
        private string _email;
       
        public DonorProfile(string id)
        {
            InitializeComponent();
            GetDonorProfile(id);

        }

        public async void GetDonorProfile(string id)
        {
            ApiService apiService = new ApiService();
            var donor = await apiService.GetDonor(id);
            var bloodType = await apiService.GetBloodType(id);
            var organType = await apiService.GetOrganType(id);

            ImgProfile.Source = donor.FullLogoPath;
            LblName.Text = donor.FirstName +  " " + donor.LastName;
            LblLocation.Text = donor.Location;
            Lblblood.Text = bloodType.BloodType;
            Lstvw.ItemsSource = organType;
            _email = donor.Email;
            _number = donor.Phone;
        }

        private void BtnCall_OnClicked(object sender, EventArgs e)
        {
            PhoneDialer.Open(_number);
        }

        private void BtnSms_OnClicked(object sender, EventArgs e)
        {
            var message = new SmsMessage("", _number);
            Sms.ComposeAsync(message);
        }

        private void BtnEmail_OnClicked(object sender, EventArgs e)
        {
            var message = new EmailMessage("", "", Settings.Email);
            Email.ComposeAsync(message);
        }
    }
}