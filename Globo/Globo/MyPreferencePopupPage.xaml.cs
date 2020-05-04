using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Globo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPreferencePopupPage : PopupPage
    {
        public MyPreferencePopupPage()
        {
            InitializeComponent();
        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);

        }
    }
}