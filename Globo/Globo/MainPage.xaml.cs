using Globo.Model;
using Newtonsoft.Json;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Globo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private const string url = "https://api.rss2json.com/v1/api.json?rss_url=http%3A%2F%2Fg1.globo.com%2Fdynamo%2Frss2.xml";

        public MainPage()
        {
            InitializeComponent();
            _ = LoadData();
            collectionView.IsVisible = false;
            SharedTransitionNavigationPage.SetBackgroundAnimation(this, BackgroundAnimation.Fade);
            SharedTransitionNavigationPage.SetTransitionDuration(this, 500);
        }


        public async Task LoadData()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri(url));
        }

        public void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Notice data = JsonConvert.DeserializeObject<Notice>(e.Result);
            collectionView.ItemsSource = data.Items;
            abso.IsVisible = false;
            collectionView.IsVisible = true;
            collectionView.SelectedItem = data.Items;
            collectionView.SelectionChanged += OnCollectionViewSelectionChanged;

        }
        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SharedTransitionNavigationPage.SetTransitionSelectedGroup(this, "Banner");

            var item = e.CurrentSelection[0];
           Item obj2 = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(item));

            await Navigation.PushAsync(new DetailNotice(obj2));
        }
    }
}
