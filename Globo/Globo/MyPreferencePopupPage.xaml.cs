using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _ImageCount = 500;
        public ObservableCollection<object> Items { get; }

        public MyPreferencePopupPage()
        {
            InitializeComponent();
            BindingContext = this;

            Items = new ObservableCollection<object>
            {
                new { Source = CreateSource(), Ind = _ImageCount++, Color = Color.Red },
                new { Source = CreateSource(), Ind = _ImageCount++, Color = Color.Green },
                new { Source = CreateSource(), Ind = _ImageCount++, Color = Color.Gold },
                new { Source = CreateSource(), Ind = _ImageCount++, Color = Color.Silver },
                new { Source = CreateSource(), Ind = _ImageCount++, Color = Color.Blue }
            };
        }

        private string CreateSource()
        {
            return $"https://picsum.photos/500/500?image={_ImageCount}";
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);

        }
    }
}