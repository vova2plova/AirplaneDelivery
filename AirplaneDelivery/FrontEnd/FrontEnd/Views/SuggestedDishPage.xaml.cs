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
    public partial class SuggestedDishPage : ContentPage
    {
        SuggestedDishViewModel vm = new SuggestedDishViewModel();
        public SuggestedDishPage()
        {
            InitializeComponent();
            this.BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            vm.LoadRecipes(List);
            base.OnAppearing();
        }
    }
}