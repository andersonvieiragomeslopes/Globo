using BottomTabBar;
using Globo.Model;
using Globo.Themes;
using Newtonsoft.Json;
using Plugin.SharedTransitions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Globo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage : ContentPage
    {
        private const string url = "https://api.rss2json.com/v1/api.json?rss_url=http%3A%2F%2Fg1.globo.com%2Fdynamo%2Frss2.xml";

        #region Properties

        bool _isLightTheme;
        public bool IsLightTheme {
            get { return _isLightTheme; }
            set {
                _isLightTheme = value;
                OnPropertyChanged();
                if (IsLightTheme)
                {
                    IsDarkTheme = false;
                    IsSystemPreferredTheme = false;
                }
            }
        }

        bool _isDarkTheme;
        public bool IsDarkTheme {
            get { return _isDarkTheme; }
            set {
                _isDarkTheme = value;
                OnPropertyChanged();
                if (IsDarkTheme)
                {
                    IsLightTheme = false;
                    IsSystemPreferredTheme = false;
                }
            }
        }

        bool _isSystemPreferredTheme;
        public bool IsSystemPreferredTheme {
            get { return _isSystemPreferredTheme; }
            set {
                _isSystemPreferredTheme = value;
                OnPropertyChanged();
                if (IsSystemPreferredTheme)
                {
                    IsLightTheme = false;
                    IsDarkTheme = false;
                }
            }
        }


        #endregion

        public TabbedPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            // GetThemeSetting();
            List<TabItem> tabs = new List<TabItem>();
            tabs.Add(new TabItem() { Id = 1, Icon = "ic_home.png", SelectedIcon = "ic_logo.png", Name = "Inicio", });
            tabs.Add(new TabItem() { Id = 2, Icon = "ic_list.png", SelectedIcon = "ic_logo.png", Name = "Lista", });
            tabs.Add(new TabItem() { Id = 3, Icon = "ic_search.png", SelectedIcon = "ic_logo.png", Name = "Buscar", });
            tabs.Add(new TabItem() { Id = 4, Icon = "ic_profile.png", SelectedIcon = "ic_logo.png", Name = "Perfil" });

            BottomTabBarContainer.TabItemsSource = tabs;
            _ = LoadData();
            stack.IsVisible = false;
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
            //abso.IsVisible = false;
            stack.IsVisible = true;
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




        private async void BottomTabBarContainer_Tapped(object sender, BottomTabBar.TabItem e)
        {

            if (e.Id == 1)
            {
                stack.IsVisible = true;
                GloboList.IsVisible = false;
                GloboProfile.IsVisible = false;
                GloboSearch.IsVisible = false;
            }
            else if (e.Id == 2)
            {
                stack.IsVisible = false;
                GloboList.IsVisible = true;
                GloboProfile.IsVisible = false;
                GloboSearch.IsVisible = false;
                await Navigation.PushPopupAsync(new MyPreferencePopupPage());


            }
            else if (e.Id == 3)
            {
                stack.IsVisible = false;
                GloboList.IsVisible = false;
                GloboProfile.IsVisible = false;
                GloboSearch.IsVisible = true;
            }
            else if (e.Id == 4)
            {
                stack.IsVisible = false;
                GloboList.IsVisible = false;
                GloboProfile.IsVisible = true;
                GloboSearch.IsVisible = false;
            }
        }

        private void SelectType(object sender, EventArgs e)
        {
            var view = sender as View;
            var parent = view.Parent as StackLayout;

            foreach (var child in parent.Children)
            {
                VisualStateManager.GoToState(child, "Normal");
                ChangeTextColor(child, "#121212");
            }

            VisualStateManager.GoToState(view, "Selected");
            ChangeTextColor(view, "#FFFFFF");
        }
        private void ChangeTextColor(View child, string hexColor)
        {
            var txtCtrl = child.FindByName<Label>("PropertyTypeName");

            if (txtCtrl != null)
                txtCtrl.TextColor = Color.FromHex(hexColor);
        }

        public List<PropertyType> PropertyTypeList => GetPropertyTypes();

        // inspired by devcrux hotel
        //mencionar nos créditos
        private List<PropertyType> GetPropertyTypes()
        {
            return new List<PropertyType>
            {
                new PropertyType { TypeName = "Todos" },
                new PropertyType { TypeName = "Notícias" },
                new PropertyType { TypeName = "Tecnologia" },
                new PropertyType { TypeName = "Filmes" }
            };
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var theme = args.Parameter.ToString();
            var appTheme = Enums.ConvertToEnum<Settings.Theme>(theme);

            switch (appTheme)
            {
                case Settings.Theme.LightTheme:
                    IsLightTheme = true;
                    ThemeHelper.ChangeToLightTheme();
                    break;
                case Settings.Theme.DarkTheme:
                    IsDarkTheme = true;
                    ThemeHelper.ChangeToDarkTheme();
                    break;
                case Settings.Theme.SystemPreferred:
                    IsSystemPreferredTheme = true;
                    ThemeHelper.ChangeToSystemPreferredTheme();
                    break;
                default:
                    IsSystemPreferredTheme = true;
                    ThemeHelper.ChangeToSystemPreferredTheme();
                    break;
            }
        }
    }

    public class PropertyType
    {
        public string TypeName { get; set; }
    }
}