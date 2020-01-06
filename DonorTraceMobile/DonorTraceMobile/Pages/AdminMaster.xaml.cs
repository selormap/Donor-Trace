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
    public partial class AdminMaster : MasterDetailPage
    {
        public AdminMaster()
        {
            InitializeComponent();
        }

        private void TapHome_OnTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TapFacility_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AddFacility());
            IsPresented = false;
        }

        private void TapDoctor_OnTapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AddOfficer());
            IsPresented = false;
        }

        private void TapLogout_OnTapped(object sender, EventArgs e)
        {
           
                Settings.Email = string.Empty;
                Settings.Password = string.Empty;
                Settings.Token = string.Empty;

                Application.Current.MainPage = new NavigationPage(new LoginPage());
            
        }

        private void TapCampaign_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AddCampaign());
            IsPresented = false;
        }

        private void TapFacilities_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Facilities());
            IsPresented = false;
        }

        private void TapUsers_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new FacilityUsers());
            IsPresented = false;
        }

        private void TapCampaigns_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Campaigns());
            IsPresented = false;
        }
    }
}