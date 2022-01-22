using Frontend.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Frontend.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}