using DonorTraceMobile.Models;
using DonorTraceMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacilityUsers : ContentPage
    {
        public ObservableCollection<OfficerModel> facilityUsers;
        private bool First = true;
        public FacilityUsers()
        {
            InitializeComponent();
            facilityUsers = new ObservableCollection<OfficerModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (First)
            {
                ApiService apiService = new ApiService();
                var users = await apiService.GetOfficers();

                foreach (var user in users)
                {
                    facilityUsers.Add(user);
                }

                LstUsers.ItemsSource = facilityUsers;
                Overlay.IsVisible = false;
            }

            First = false;

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            LstUsers.ItemsSource = string.IsNullOrEmpty(e.NewTextValue) ? facilityUsers : facilityUsers
              .Where(x => x.Name.ToLower().StartsWith(e.NewTextValue.ToLower())
              || x.Facility.ToLower().Equals(e.NewTextValue.ToLower()));
        }

        private void LstUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}