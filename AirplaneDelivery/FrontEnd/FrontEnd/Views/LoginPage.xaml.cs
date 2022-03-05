using Acr.UserDialogs;
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

        public async void ok()
        {

            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();

        }

        private void Enter(object sender, EventArgs e)
        {
            var user = new User()
            {
                Name = login.Text,
                Password = password.Text

            };
            _vm.EnterCommand.Execute(user);
           
        }
        private void Enter2(object sender, EventArgs e)
        {
            if (passwordNew.Text == passwordRepeat.Text)
            {
                var user = new User()
                {
                    Name = loginNew.Text,
                    Password = passwordNew.Text
                };
                _vm.EnterCommand2.Execute(user);
            }
            else
            {
                UserDialogs.Instance.Toast("Введенные пароли не одинаковые!");
            }
        }

        private void OpenSignUp()
        {
            
            SignUpFrame.IsVisible = true;

            Action<double> callback = input => SignUpView.TranslationX = input;
            SignUpView.Animate("anim", callback, -260, 0, 16, 300, Easing.CubicInOut);
        }

        public void CloseSignUp()
        {
            Action<double> callback = input => SignUpView.TranslationX = input;
            SignUpView.Animate("anim", callback, 0, -260, 16, 300, Easing.CubicInOut);
            
            SignUpFrame.IsVisible = false;
        }

        
        private void SignUpTapped(object sender, EventArgs e)
        {
            OpenSignUp();
        }

        private void OverlayTapped(object sender, EventArgs e)
        {
           CloseSignUp();
        }
    }
}