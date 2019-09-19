using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceMobile.Models;
using DonorTraceMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorTraceMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Donors : ContentPage
    {
        public ObservableCollection<DonorModel> DonorList;
        private bool First = true;
        public Donors()
        {
            InitializeComponent();
            DonorList = new ObservableCollection<DonorModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (First)
            {
                ApiService apiService = new ApiService();
                var donors = await apiService.GetDonors();

                foreach (var donor in donors)
                {
                    DonorList.Add(donor);
                }

                LstDonors.ItemsSource = DonorList;
                BusyIndicator.IsRunning = false;
            }

            First = false;


        }

        private void LstDonors_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is DonorModel selectedDonor) Navigation.PushAsync(new DonorProfile(selectedDonor.Id));
            ((ListView) sender).SelectedItem = null;
        }

        private void TbSearch_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindDonor());
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //BusyIndicator.IsRunning = true;
            LstDonors.ItemsSource = string.IsNullOrEmpty(e.NewTextValue) ? DonorList : DonorList
                .Where(x => x.Gender.ToLower().StartsWith(e.NewTextValue.ToLower()) 
                            || x.BloodType.ToLower().Equals(e.NewTextValue.ToLower()) 
                            || x.Region.ToLower().StartsWith(e.NewTextValue.ToLower())
                            || x.DonorOrgans.ElementAtOrDefault(0).Name.ToLower().Equals(e.NewTextValue.ToLower()));

           // BusyIndicator.IsRunning = false;
        }
    }
}