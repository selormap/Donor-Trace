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
    public partial class FacilityMaster : MasterDetailPage
    {
        public FacilityMaster()
        {
            InitializeComponent();
            GetProfile(Settings.Id);
        }

        public async void GetProfile(string id)
        {
            ApiService apiService = new ApiService();
            var officer = await apiService.GetOfficer(id);
            LblName.Text = "Hello" + " " + officer.Name.ToUpper();
        }

        private void TapHome_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new FacilityHome());
            IsPresented = false;
        }

        private void TapChangePass_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ChangePassword());
            IsPresented = false;
        }

        private void TapDonors_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Donors());
            IsPresented = false;
        }

        private void TapLogout_OnTapped(object sender, EventArgs e)
        {
            Settings.Email = string.Empty;
            Settings.Password = string.Empty;
            Settings.Token = string.Empty;
            Settings.Role = string.Empty;

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}