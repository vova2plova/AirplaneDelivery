﻿using Frontend.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Frontend.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RedirectRegister { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RedirectRegister = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
