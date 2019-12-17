using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHome : ContentPage
    {
        public AdminHome()
        {
            InitializeComponent();
        }

        private async void BtnMap_OnClicked(object sender, EventArgs e)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);

            //if (location != null)
           // {
                // await Launcher.OpenAsync("geo:0,0?q=").ConfigureAwait(false);
               // var location = new Location(47.645160, -122.1306032);
               // var options = new MapLaunchOptions { Name = "Kwabenya" };
               
                await Map.OpenAsync(location);
           // }
           // else
           // {
                //await Map.OpenAsync("ghana");
           // }

            


        }

        private async void BtnGet_OnClicked(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            await DisplayAlert("Coordinates", location.Longitude + " " + location.Latitude, "OK");
        }
    }
}