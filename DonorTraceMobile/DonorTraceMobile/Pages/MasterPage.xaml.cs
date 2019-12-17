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
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            GetProfile(Settings.Id);
            DonorExist(Settings.Id);
            
        }

        public async void GetProfile(string id)
        {
            ApiService apiService = new ApiService();
            var donor = await apiService.GetDonor(id);
            LblName.Text = (donor != null) ? "Hello" + " " + donor.Name.ToUpper() : "Hello" + " " + Settings.Email;
            //if (donor != null) LblName.Text = "Hello" + " " + donor.Name.ToUpper();
        }

        public async void DonorExist(string id)
        {
            ApiService apiService = new ApiService();
            var donor = await apiService.DonorExist(id);
            if (donor == false)
            {
                StkDonors.IsVisible = true;
            }
            else
            {
                StkDonors.IsVisible = false;
            }
           // StkDonors.IsVisible = !donor;
        }

        private void BtnHome_Clicked(object sender, EventArgs e)
        {
           // Detail = new NavigationPage(new HomePage());
            //IsPresented = false;
        }

        private void TapHome_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        private void TapDonor_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new BecomeDonor());
            IsPresented = false;
        }

        private void TapChangePass_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ChangePassword());
            IsPresented = false;
        }

        private void TapLogout_Tapped(object sender, EventArgs e)
        {
            Settings.Email = string.Empty;
            Settings.Password = string.Empty;
            Settings.Token = string.Empty;

            Application.Current.MainPage = new NavigationPage (new LoginPage());
        }

        private void TapDonors_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Donors());
            IsPresented = false;
        }

        private void TapProfile_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MyProfile());
            IsPresented = false;
        }
    }
}