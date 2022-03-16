using DAL.Models;
using FrontEnd.OnlineServices;
using FrontEnd.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrontEnd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private ProfilePageViewModel _vm = new ProfilePageViewModel();
        
        public ProfilePage()
        {
            Title = "Профиль";
            
            InitializeComponent();
           
        }

        
       async private void Init()
        {
            var user = await MainService.UserService.GetUserById(Preferences.Get("user_id", 0));
            
            name.Text = user.Content.Name;
            phone.Text = user.Content.Number;
            address.Text = user.Content.Address;
        }
        protected override async void OnAppearing()
        {
            Init();
        }
       async private void SaveCommand(object sender, EventArgs e)
        {
            var _user = await MainService.UserService.GetUserById(Preferences.Get("user_id", 0));
            var user = new User()
            {
                Id = _user.Content.Id,
                Name = name.Text,
                Number = phone.Text
                
            };
            _vm.SaveCommand.Execute(user);
            
        }
    }
}