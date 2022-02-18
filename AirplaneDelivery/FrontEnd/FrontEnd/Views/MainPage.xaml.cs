using FrontEnd.ViewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
        protected override async void OnAppearing()
        {
            await _vm.LoadData();
            base.OnAppearing();
        }

        
    }
}
