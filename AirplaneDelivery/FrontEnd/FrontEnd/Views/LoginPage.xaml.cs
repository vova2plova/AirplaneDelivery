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
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _vm = new LoginPageViewModel();
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
         private async  void Enter(object sender, EventArgs e)
        {
            string loginTxt = login.Text;
            string pass = password.Text;
            var getLoginDetails = await _vm.SignInUser(loginTxt, pass);
            if (getLoginDetails != null)
            {
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
               
            }


        }
    }
}