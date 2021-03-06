﻿using DonorTraceMobile.Models;
using DonorTraceMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using DonorTraceMobile.Helpers;
using Plugin.InputKit;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Graphics;
using Android.Views;
using Caliburn.Micro;
using Xamarin.Essentials;
using Point = System.Drawing.Point;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BecomeDonor : ContentPage
    {
        private MediaFile _file;
        private List<int> _organList = new List<int>();
        public BecomeDonor()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            ApiService apiServices = new ApiService();

            Lst.ItemsSource = await apiServices.OrganList();
            Reg.ItemsSource = await apiServices.GetRegions();
            EntBloodGroup.ItemsSource = await apiServices.GetBloodGroups();
            base.OnAppearing();

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

            Org.IsVisible = !Org.IsVisible;
        }

        private void ChkBlood_CheckChanged(object sender, EventArgs e)
        {
            Bld.IsVisible = !Bld.IsVisible;
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
                    

                    EntBloodType.ItemsSource = await apiServices.GetBloodType(id);
                }
            }
        }

        private async void BtnRegister_OnClicked(object sender, EventArgs e)
        {

            Overlay.IsVisible = true;

            if (_file == null)
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Image Upload", "Upload your image", "Ok");
                return;
            }

            if (!ChkBlood.IsChecked && !ChkOrgan.IsChecked)
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Donate Option", "Select at least one donation option", "Ok");
                return;
            }

            if (EntBloodGroup.SelectedIndex == -1 && _organList.Count == 0)
            {
                Overlay.IsVisible = false;
                await DisplayAlert("", "Select at least a blood group or an organ", "OK");
            }


            var imageArray = FilesHelper.ReadFully(_file.GetStream());
            _file.Dispose();

            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            var donor = new Donor()
            {
                Id = Settings.Id,
                FirstName = EntFirstname.Text,
                LastName = EntLastname.Text,
                Phone = EntPhone.Text,
                Gender = RdGender.SelectedItem.ToString(),
                RegionId = ((RegionModel)Reg.SelectedItem).Id,
                Location = placemark.Locality,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Email = Settings.Email,
                ImageArray = imageArray

            };


            ApiService apiService = new ApiService();
            var response = await apiService.BecomeADonor(donor);

            if (!response)
            {
                Overlay.IsVisible = false;
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }

            else
            {
                var bloodOption = new BloodOrganOption();
                if (EntBloodType.SelectedIndex != -1)
                {
                    bloodOption.UserId = Settings.Id;
                    bloodOption.BloodTypeId = ((BloodType)EntBloodType.SelectedItem).Id;
                   //bloodOption.OrganListId = (int?)null;
                    await apiService.AddBloodOrganOption(bloodOption);
                }

                if (_organList.Count > 0)
                {
                    var organOption = new OrganOption();
                    foreach (var option in _organList)
                    {

                        organOption.UserId = Settings.Id;
                       // organOption.BloodTypeId = int ?;
                        organOption.OrganListId = option;
                        await apiService.AddOrganOption(organOption);
                    }
                    
                }

                Overlay.IsVisible = false;
                //  var result = await DisplayAlert("Success", "You are now a registered donor", "Ok");
                // if (result == true)
                // {
                await DisplayAlert("Success", "You are now a registered donor", "Ok");
                // await Navigation.PushAsync(new HomePage());
                Application.Current.MainPage = new MasterPage();
                // }

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