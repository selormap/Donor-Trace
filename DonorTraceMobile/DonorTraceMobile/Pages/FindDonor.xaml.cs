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
    public partial class FindDonor : ContentPage
    {
        public ObservableCollection<RegionModel> Regions;
        public ObservableCollection<BloodType> BloodTypes;
        public ObservableCollection<OrganList> Organs;
        ApiService apiService = new ApiService();

        public FindDonor()
        {
            InitializeComponent();
            Regions = new ObservableCollection<RegionModel>();
            BloodTypes = new ObservableCollection<BloodType>();
            Organs = new ObservableCollection<OrganList>();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            LoadRegions();
            LoadOrgans();
            LoadBloodTypes();

        }

        public async void LoadRegions()

        {

            var regions = await apiService.GetRegions();

            foreach (var region in regions)

            {

                Regions.Add(region);

            }


            PckRegion.ItemsSource = Regions;

        }

        public async void LoadBloodTypes()
        {

            var bloodTypes = await apiService.GetBloodTypes();

            foreach (var bloodType in bloodTypes)
            {

                BloodTypes.Add(bloodType);

            }

            PckGroup.ItemsSource = BloodTypes;
        }

        public async void LoadOrgans()
        {

            var organs = await apiService.OrganList();

            foreach (var organ in organs)
            {

                Organs.Add(organ);

            }

            PckOrgan.ItemsSource = Organs;
        }

        private void BtnSearch_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}