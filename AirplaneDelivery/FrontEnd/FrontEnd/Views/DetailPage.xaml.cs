using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using FrontEnd.ViewsModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrontEnd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        private DetailPageViewModel vm { get; set; }
        public DetailPage(Product product)
        {
            vm = new DetailPageViewModel(product);
            InitializeComponent();
            this.BindingContext = vm;
        }

        private void Dec_Tapped(object sender, EventArgs e)
        {
            vm.DecCount();
        }

        private void Inc_Tapped(object sender, EventArgs e)
        {
            vm.IncCount();
        }

        private void AddSlotToCart_Tapped(object sender, EventArgs e)
        {
            vm.AddSlotToCart();
        }
    }
}