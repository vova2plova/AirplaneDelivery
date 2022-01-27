using Frontend.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Frontend.ViewModels
{
    internal class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }
        public Command RouteToLogin { get; }

        public RegisterViewModel()
        {
            RouteToLogin = new Command(OnLogginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private async void OnLogginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
