using FrontEnd.ViewsModels;
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
    public partial class CartPage : ContentPage
    {
        CartViewModel vm = new CartViewModel();
        public CartPage()
        {
            this.BindingContext = vm;
            InitializeComponent();
            vm.LoadCart(List);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void AddToHistory_Tapped(object sender, EventArgs e)
        {
            vm.AddToHistory();
        }
    }
}