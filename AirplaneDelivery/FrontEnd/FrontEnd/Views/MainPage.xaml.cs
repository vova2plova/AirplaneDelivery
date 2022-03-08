using FrontEnd.Views;
using FrontEnd.ViewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FrontEnd
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _vm = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _vm;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void OpenMenu()
        {
            MenuGrid.IsVisible = true;

            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, -260, 0, 16, 300, Easing.CubicInOut);
        }

        private void CloseMenu()
        {
            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, 0, -260, 16, 300, Easing.CubicInOut);

            MenuGrid.IsVisible = false;
        }


        private void MenuTapped(object sender, EventArgs e)
        {
            OpenMenu();
        }

        private void OverlayTapped(object sender, EventArgs e)
        {
            CloseMenu();
        }
        private void SeachTapped(object sender, EventArgs e)
        {
            searchIcon.IsVisible = false;
            searchbar.IsVisible = true;
        }
        private void SeachAnTapped(object sender, EventArgs e)
        {
            searchIcon.IsVisible = true;
            searchbar.IsVisible = false;
            
        }

      
    }
}
