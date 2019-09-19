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
    public partial class AdminMaster : MasterDetailPage
    {
        public AdminMaster()
        {
            InitializeComponent();
        }

        private void TapHome_OnTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TapFacility_OnTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TapDoctor_OnTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}