using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrontEnd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DishesMainPage : ContentPage
    {
        public DishesMainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();
        }
     
    }
}