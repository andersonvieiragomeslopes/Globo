﻿using BottomTabBar;
using Globo.Model;
using Newtonsoft.Json;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Globo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage : ContentPage
    {
        private const string url = "https://api.rss2json.com/v1/api.json?rss_url=http%3A%2F%2Fg1.globo.com%2Fdynamo%2Frss2.xml";

        public TabbedPage()
        {
            InitializeComponent();
            List<TabItem> tabs = new List<TabItem>();
            tabs.Add(new TabItem() { Id = 1, Icon = "ic_home.png", SelectedIcon = "ic_logo.png", Name = "Inicio", });
            tabs.Add(new TabItem() { Id = 2, Icon = "ic_list.png", SelectedIcon = "ic_logo.png", Name = "Lista", });
            tabs.Add(new TabItem() { Id = 3, Icon = "ic_search.png", SelectedIcon = "ic_logo.png", Name = "Buscar", });
            tabs.Add(new TabItem() { Id = 4, Icon = "ic_profile.png", SelectedIcon = "ic_logo.png", Name = "Perfil" });

            BottomTabBarContainer.TabItemsSource = tabs;
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



        //private async void BottomTabBarContainer_Tapped(object sender, BottomTabBar.TabItem e)
        //{
        //    var mainViewModel = MainViewModel.GetInstance();

        //    if (e.Id == 1)
        //    {
        //        //mainViewModel.AllNotices = new AllNoticeViewModel();
        //        //await Navigation.PushAsync(new AllNoticesView());
        //    }
        //    else if (e.Id == 2)
        //    {
        //        mainViewModel.AllChannels = new AllChannelsViewModel();
        //        await Navigation.PushAsync(new AllChannelView());

        //    }
        //    else if (e.Id == 3)
        //    {
        //        if (AppSettings.IsLive)
        //        {
        //            await Navigation.PushAsync(new LiveStreamingView(AppSettings.TokenType));
        //        }
        //        else
        //        {
        //            App.ShowToast(ToastNotificationType.Warning, "Que pena!", "No momento, não há jogos ao vivo!", 5);

        //        }
        //    }
        //    else if (e.Id == 4)
        //    {
        //        mainViewModel.Games = new GamesViewModel();
        //        await Navigation.PushAsync(new GamesView());
        //    }
        //    else if (e.Id == 5)
        //    {
        //        mainViewModel.TableGames = new TableGameViewModel();
        //        await Navigation.PushAsync(new TableGamesView());
        //    }
        //}
    }
}