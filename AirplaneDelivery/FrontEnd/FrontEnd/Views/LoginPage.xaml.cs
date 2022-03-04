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
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _vm = new LoginPageViewModel();
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

         public async  void ok()
        {
        
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
     
        }

        private void Enter(object sender, EventArgs e)
        {
            _vm.EnterCommand.Execute(new User()
            {
                Name = login.Text,
                Password = password.Text
                
            });
            Console.WriteLine(login.Text, password.Text);
        }
    }
}