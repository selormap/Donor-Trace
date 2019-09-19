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
    public partial class FacilityHome : ContentPage
    {
        public FacilityHome()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetOfficerProfile(Settings.Id);
        }

        public async void GetOfficerProfile(string id)
        {
            ApiService apiService = new ApiService();
            var officer = await apiService.GetOfficer(id);

            LblName.Text = "Hello" + " " + officer.Name;
            LblFacility.Text = "Facility:" + " " + officer.Facility;

        }
    }
}