using Acr.UserDialogs;
using DAL.Models;
using DLToolkit.Forms.Controls;
using FrontEnd.OnlineServices;
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

        public ObservableCollection<Category> Categories;
        public ObservableCollection<Product> Products;

        private Command<int> _selectMenuCommand;
        private ObservableCollection<Menu> menuList;
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }
        public Command<Category> SelectCategoryCommand => new Command<Category>(Category =>
        {
            Application.Current.MainPage.DisplayAlert("Selected Plan", "название плана : " + Category.Title, "Ok");
        });

        public Command<Product> SelectProductCommand => new Command<Product>(Product =>
        {
            Application.Current.MainPage.DisplayAlert("Selected Plan", "название плана : " + Product.Name, "Ok");
        });


        private ObservableCollection<Menu> GetMenus()
        { 
            return new ObservableCollection<Menu>
            {
                new Menu {Id=1, Icon = "order.png", Name = "Мои заказы"},
                new Menu {Id=2, Icon = "settings.png", Name = "Мои данные"},
                new Menu{Id=3 ,Icon = "settings.png", Name = "Выйти из аккаунта"}
            };
        }

        public async void LoadProducts(FlowListView ProdutList)
        {
            var response = await MainService.ProductService.GetAllProducts();
            if (response.IsSuccessStatusCode)
            {
                Products = new ObservableCollection<Product>(response.Content);
                ProdutList.FlowItemsSource = Products;
            }
        }

        public Command ItemTappedCommand
        {
            get
            {
                return new Command((data) =>
                {
                    Application.Current.MainPage.DisplayAlert("FlowListView", data + "", "Ok");
                });
            }
        }

        public async void LoadCategory(CollectionView CategoryList)
        {
            var response = await MainService.CategoryService.GetCategories();
            if (response.IsSuccessStatusCode)
            {
                Categories = new ObservableCollection<Category>(response.Content);
                CategoryList.ItemsSource = Categories;
            }
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
