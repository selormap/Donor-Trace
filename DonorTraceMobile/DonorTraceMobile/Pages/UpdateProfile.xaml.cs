using DonorTraceMobile.Helpers;
using DonorTraceMobile.Models;
using DonorTraceMobile.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfile : ContentPage
    {

        private MediaFile _file;
        private List<int> _organList = new List<int>();
        ApiService apiServices = new ApiService();
        public UpdateProfile()
        {
            InitializeComponent();
            GetRegions();
            GetProfile(Settings.Id);
        }

        protected override async void OnAppearing()
        {
            ApiService apiServices = new ApiService();

            //Lst.ItemsSource = await apiServices.OrganList();
          //  Reg.ItemsSource = await apiServices.GetRegions();
            //EntBloodGroup.ItemsSource = await apiServices.GetBloodGroups();
            base.OnAppearing();

        }
        public async void GetRegions()
        {
            Reg.ItemsSource = await apiServices.GetRegions();
        }

        public async void GetProfile(string id)
        {
            ApiService apiService = new ApiService();
            var donor = await apiService.GetDonor(id);
           // var bloodType = await apiService.GetBloodType(id);
            //var organType = await apiService.GetOrganType(id);

            ImgProfile.Source = donor.FullLogoPath;
            EntFirstname.Text = donor.FirstName;
            EntLastname.Text = donor.LastName;
            //LblEmail.Text = donor.Email;
            RdGender.SelectedItem = donor.Gender;
            EntPhone.Text = donor.Phone;
            
            Reg.SelectedIndex = donor.RegionId - 1;
            //LblLocation.Text = donor.Location;
            //LblBlood.Text = bloodType.BloodType;
            // Lst.ItemsSource = organType;
            Overlay.IsVisible = false;

        }

        private async void TapCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();



            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            _file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {

                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 30, //Resize to 90% of original
                CompressionQuality = 60,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (_file == null)
                return;

            await DisplayAlert("File Location", _file.Path, "OK");

            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = _file.GetStream();
                return stream;
            });
        }

        private void ChkOrgan_CheckChanged(object sender, EventArgs e)
        {

            //Org.IsVisible = !Org.IsVisible;
        }

        private void ChkBlood_CheckChanged(object sender, EventArgs e)
        {
           // Bld.IsVisible = !Bld.IsVisible;
        }

        private async void EntBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker entBloodGroup = (Picker)sender;
            int selectedIndex = entBloodGroup.SelectedIndex;

            ApiService apiServices = new ApiService();


            if (selectedIndex != -1)
            {
                if (entBloodGroup.SelectedItem is BloodGroup selectedItem)
                {
                    int id = selectedItem.Id;


                    //EntBloodType.ItemsSource = await apiServices.GetBloodType(id);
                }
            }
        }

        private async void BtnRegister_OnClicked(object sender, EventArgs e)
        {

            Overlay.IsVisible = true;


            //if (!ChkBlood.IsChecked && !ChkOrgan.IsChecked)
            //{
            //    Overlay.IsVisible = false;
            //    await DisplayAlert("Donate Option", "Select at least one donation option", "Ok");
            //    return;
            //}

            //if (EntBloodGroup.SelectedIndex == -1 && _organList.Count == 0)
            //{
            //    Overlay.IsVisible = false;
            //    await DisplayAlert("", "Select at least a blood group or an organ", "OK");
            //}

            ApiService apiService = new ApiService();



            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();

            if (_file == null)
            {
                var donor = new Donor()
                {
                    Id = Settings.Id,
                    FirstName = EntFirstname.Text,
                    LastName = EntLastname.Text,
                    Phone = EntPhone.Text,
                    Gender = RdGender.SelectedItem.ToString(),
                    RegionId = ((RegionModel)Reg.SelectedItem).Id,
                    Location = placemark.Locality,// placemark.AdminArea,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,

                };


                var response = await apiService.UpdateDonor(Settings.Id, donor);

                if (!response)
                {
                    Overlay.IsVisible = false;
                    await DisplayAlert("Oops", "Something went wrong", "Cancel");
                }

                else
                {

                    Overlay.IsVisible = false;
                    //  var result = await DisplayAlert("Success", "You are now a registered donor", "Ok");
                    // if (result == true)
                    // {
                    await DisplayAlert("Success", "Profile Updated Successfully", "Ok");
                    await Navigation.PushAsync(new MyProfile());
                   // Application.Current.MainPage = new MyProfile();
                    // }

                }
            }

            else
            {


                var imageArray = FilesHelper.ReadFully(_file.GetStream());
                _file.Dispose();

                var donor = new Donor()
                {
                    Id = Settings.Id,
                    FirstName = EntFirstname.Text,
                    LastName = EntLastname.Text,
                    Phone = EntPhone.Text,
                    Gender = RdGender.SelectedItem.ToString(),
                    RegionId = ((RegionModel)Reg.SelectedItem).Id,
                    Location = placemark.AdminArea,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    ImageArray = imageArray
                };

                var response = await apiService.UpdateDonor(Settings.Id, donor);

                if (!response)
                {
                    Overlay.IsVisible = false;
                    await DisplayAlert("Oops", "Something went wrong", "Cancel");
                }

                else
                {

                    Overlay.IsVisible = false;
                    //  var result = await DisplayAlert("Success", "You are now a registered donor", "Ok");
                    // if (result == true)
                    // {
                    await DisplayAlert("Success", "Profile Updated Successfully", "Ok");
                    await Navigation.PushAsync(new MyProfile());
                  //  Application.Current.MainPage = new NavigationPage(new MyProfile());
                    // }

                }
            }
        }

        private void ChBox_OnCheckChanged(object sender, EventArgs e)
        {
            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;

            if (checkbox.BindingContext is OrganList ob)

            {

                if (checkbox.IsChecked)
                {
                    _organList.Add(ob.Id);
                    // DisplayAlert("", organList.ToString(), "Ok");
                }

            }
        }
    }
}