using Acr.UserDialogs;
using DAL.Models;
using FrontEnd.OnlineServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace FrontEnd.ViewsModels
{
    internal class DetailDishViewModel : INotifyPropertyChanged
    {
        private static Recipe _recipe;
        public ObservableCollection<Product> Products;
        private int _SumPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        public DetailDishViewModel(Recipe recipe)
        {
            _recipe = recipe;
        }

        public async void LoadProductFromRecipe(StackLayout List)
        {
            var products = await MainService.RecipeService.GetAllProductsFromRecipe(_recipe.Id);
            if (products.IsSuccessStatusCode)
            {
                Products = new ObservableCollection<Product>(products.Content);
                BindableLayout.SetItemsSource(List, Products);
                GetSumPrice();
            }
        }
        public string Title
        {
            get => _recipe.Title;
        }

        public ImageSource Image
        {
            get => ImageSource.FromUri(_recipe.Image);
        }
        
        public string UrlLink
        {
            get => _recipe.UrlLink.ToString();
        }
       
        public async void AddProductsToCart()
        {
            ConfirmConfig config = new ConfirmConfig();
            config.SetTitle("Добавить все продукты или недостающие?");
            config.SetCancelText("Недостающие");
            config.SetOkText("Все");
            var result = await UserDialogs.Instance.ConfirmAsync(config);
            if (result  == true)
            {
                var res = await MainService.CartService.AddProductsFromRecipe(Preferences.Get("user_id", 0), _recipe.Id);
                if (res.IsSuccessStatusCode)
                {
                    UserDialogs.Instance.Toast("Продукты добавлены в корзину");
                }
                else
                    UserDialogs.Instance.Toast("Произошла ошибка");
            }
            else
            {
                var res = await MainService.CartService.AddMissingProductsFromRecipe(Preferences.Get("user_id", 0), _recipe.Id);
                if (res.IsSuccessStatusCode)
                {
                    UserDialogs.Instance.Toast("Продукты добавлены в корзину");
                }
                else
                    UserDialogs.Instance.Toast("Произошла ошибка");
            }
        }

        public void GetSumPrice()
        {
            var sum = 0;
            foreach (var product in Products)
                sum += product.Price;
            Sum = sum.ToString();
        }

        public string Sum
        {
            get => _SumPrice.ToString() + " ₽";
            set
            {
                if (_SumPrice != Convert.ToInt32(value))
                {
                    _SumPrice = Convert.ToInt32(value);
                    OnPropertyChanged("Sum");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public async Task OpenBrowser()
        {
            
                await Browser.OpenAsync(UrlLink, BrowserLaunchMode.SystemPreferred);
          
        }
    }
}
