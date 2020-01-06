
using Plugin.Share;
using Plugin.Share.Abstractions;
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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void BtnShare_Clicked(object sender, EventArgs e)
        {
            CrossShare.Current.Share(new ShareMessage
            {
                Text = "Become a volunteer on the Donor Trace App",
               Title="Donor Trace App",
               Url="http://.donortrace.com"
            },
            new ShareOptions
            {
                ChooserTitle = "Donor Trace App",
                ExcludedUIActivityTypes = new[] { ShareUIActivityType.PostToFacebook, ShareUIActivityType.AirDrop,
                ShareUIActivityType.PostToFlickr, ShareUIActivityType.PostToVimeo, ShareUIActivityType.Print}
            });
            
        }
    }
}