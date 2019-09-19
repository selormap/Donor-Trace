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
    public partial class FacilityMaster : MasterDetailPage
    {
        public FacilityMaster()
        {
            InitializeComponent();
        }

        private void TapHome_OnTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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