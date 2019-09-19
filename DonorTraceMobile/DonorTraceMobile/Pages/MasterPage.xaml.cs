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
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            
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
    }
}