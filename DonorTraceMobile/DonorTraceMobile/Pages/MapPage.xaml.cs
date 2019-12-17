using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Essentials.Map;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            LMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                Distance.FromMiles(1)));
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
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            await DisplayAlert("Coordinates", location.Longitude + " " + location.Latitude + " " + placemark.SubAdminArea, "OK");
        }

        private void LMap_OnMapClicked(object sender, MapClickedEventArgs e)
        {
           
        }
    }
}