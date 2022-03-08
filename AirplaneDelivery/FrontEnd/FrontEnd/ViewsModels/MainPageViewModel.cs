using DAL.Models;
using FrontEnd.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FrontEnd.ViewsModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {

            MenuList = GetMenus();
        }
        private Command<int> _selectMenuCommand;
        private ObservableCollection<Menu> menuList;
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }
        private ObservableCollection<Menu> GetMenus()
        { 
            return new ObservableCollection<Menu>
            {
                new Menu {Id=1, Icon = "order.png", Name = "Мои заказы"},
                new Menu {Id=2, Icon = "settings.png", Name = "Мои данные"},
                new Menu{Id=3 ,Icon = "settings.png", Name = "Выйти из аккаунта"}
            };
        }
        public Command<int> SelectMenuCommand
        {
            get
            {
                return _selectMenuCommand ?? (_selectMenuCommand = new Command<int>((id) =>
                {
                    switch (id)
                    {
                        case 1:
                            Application.Current.MainPage.DisplayAlert("Selected item", id.ToString(), "Ok");
                            break;
                        case 2:
                            Application.Current.MainPage.DisplayAlert("Selected item", id.ToString(), "Ok");
                            break;
                        case 3:
                            Preferences.Clear();
                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            break;
                    }
                }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
