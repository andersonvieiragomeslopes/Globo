using Globo.Model;
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
    public partial class DetailNotice : ContentPage
    {
        public DetailNotice(Item item2)
        {
            InitializeComponent();
            item = item2;
            Image.Source = item.Thumbnail;
        }
        public Item item;
    }
}