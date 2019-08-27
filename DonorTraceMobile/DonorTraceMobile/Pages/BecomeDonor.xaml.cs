using DonorTraceMobile.Models;
using DonorTraceMobile.Services;
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
    public partial class BecomeDonor : ContentPage
    {
        public BecomeDonor()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            ApiService apiServices = new ApiService();

            lst.ItemsSource = await apiServices.OrganList();
            reg.ItemsSource = await apiServices.GetRegions();
            EntBloodGroup.ItemsSource = await apiServices.GetBloodGroups();
            base.OnAppearing();

        }


        private void TapCamera_Tapped(object sender, EventArgs e)
        {

        }

        private void ChkOrgan_CheckChanged(object sender, EventArgs e)
        {

            org.IsVisible = !org.IsVisible;
        }

        private void ChkBlood_CheckChanged(object sender, EventArgs e)
        {
            bld.IsVisible = !bld.IsVisible;
        }

        private async void EntBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var EntBloodGroup = (Picker)sender;
            int selectedIndex = EntBloodGroup.SelectedIndex;

            ApiService apiServices = new ApiService();


            if (selectedIndex != -1)
            {
                var selectedItem = EntBloodGroup.SelectedItem as BloodGroup;
                var Id = selectedItem.Id;
                //string bloodType = EntBloodGroup.Items[selectedIndex];
               // await DisplayAlert("Title", Id.ToString(), "Cancel");
               // int selectedValue = ((BloodGroup)EntBloodGroup.SelectedItem).Id;

               EntBloodType.ItemsSource = await apiServices.GetBloodType(Id);
            }
        }
    }
}