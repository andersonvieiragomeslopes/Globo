using Globo.Model;
using Newtonsoft.Json;
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
            NewsListView.ItemsSource = data.Items;
        }
    }
}
