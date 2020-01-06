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
    public partial class Campaigns : ContentPage
    {
        public ObservableCollection<CampaignModel> CampaignList;
        private bool First = true;
        public Campaigns()
        {
            InitializeComponent();
            CampaignList = new ObservableCollection<CampaignModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (First)
            {
                ApiService apiService = new ApiService();
                var campaigns = await apiService.GetCampaigns();

                foreach (var campaign in campaigns)
                {
                    CampaignList.Add(campaign);
                }

                LstCampaigns.ItemsSource = CampaignList;
                Overlay.IsVisible = false;
            }

            First = false;

        }

        private void LstCampaigns_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            LstCampaigns.ItemsSource = string.IsNullOrEmpty(e.NewTextValue) ? CampaignList : CampaignList
               .Where(x => x.Organization.ToLower().StartsWith(e.NewTextValue.ToLower())
               || x.Description.ToLower().Equals(e.NewTextValue.ToLower()));
        }
    }
}