using DAL.Models;
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
    public partial class DetailDishPage : ContentPage
    {
        DetailDishViewModel vm ;
        public DetailDishPage(Recipe recipe)
        {
            InitializeComponent();
            vm = new DetailDishViewModel(recipe);
            BindingContext = vm;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            vm.LoadProductFromRecipe(List);
            base.OnAppearing();
        }

        private void AddProductsToCart_Tapped(object sender, EventArgs e)
        {
            vm.AddProductsToCart();
        }
        private void OpenLink(object sender, EventArgs e)
        {
            vm.OpenBrowser();
        }
    }
}