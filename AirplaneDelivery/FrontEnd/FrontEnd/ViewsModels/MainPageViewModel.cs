using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.ViewsModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {

            MenuList = GetMenus();
        }
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
                new Menu { Icon = "order.png", Name = "Мои заказы"},
                new Menu { Icon = "settings.png", Name = "Мои данные"},
                new Menu{Icon = "settings.png", Name = "Выйти из аккаунта"}
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
