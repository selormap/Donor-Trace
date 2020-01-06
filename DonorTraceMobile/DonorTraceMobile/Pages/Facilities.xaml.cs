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
    public partial class Facilities : ContentPage
    {
        public ObservableCollection<FacilityModel> FacilityList;
        private bool First = true;
        public Facilities()
        {
            InitializeComponent();
            FacilityList = new ObservableCollection<FacilityModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (First)
            {
                ApiService apiService = new ApiService();
                var facilities = await apiService.Facilities();

                foreach (var facility in facilities)
                {
                    FacilityList.Add(facility);
                }

                LstFacilities.ItemsSource = FacilityList;
                Overlay.IsVisible = false;
            }

            First = false;

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            LstFacilities.ItemsSource = string.IsNullOrEmpty(e.NewTextValue) ? FacilityList : FacilityList
               .Where(x => x.Name.ToLower().StartsWith(e.NewTextValue.ToLower())
               || x.RegistrationNo.ToLower().Equals(e.NewTextValue.ToLower()));
        }

        private void LstFacilities_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}