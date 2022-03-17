using Acr.UserDialogs;
using DAL.Models;
using FrontEnd.OnlineServices;
using FrontEnd.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FrontEnd.ViewsModels
{
    internal class DetailPageViewModel : INotifyPropertyChanged
    {
        static Product _product;

        public event PropertyChangedEventHandler PropertyChanged;
        private int _count;
        public ImageSource Image => ImageSource.FromUri(_product.Image);
        public string Name => _product.Name;
        public string Price => _product.Price.ToString();
        public string Kkal => _product.Kkal.ToString();
        public string Proteins => _product.Proteins.ToString();
        public string Fats => _product.Fats.ToString();
        public string Carbohydrates => _product.Carbohydrates.ToString();
        public DetailPageViewModel(Product product)
        {
            _product = product;
            Count = 1.ToString();
        }

        public async void AddSlotToCart()
        {
            Spot spot = new Spot
            {
                Count = _count,
                Products = _product
            };
            var response = await MainService.CartService.AddSpotToCart(spot, Preferences.Get("user_id", 0));
            if (response.IsSuccessStatusCode)
                UserDialogs.Instance.Toast("Продукт добавлен в корзину", new TimeSpan(100));
            else
                UserDialogs.Instance.Toast("Произошла ошибка" , new TimeSpan(100));
        }

        public string Count 
        {
            get
            {
                return _count.ToString();
            }
            set 
            {
                if (_count.ToString() != value)
                {
                    _count = Convert.ToInt32(value);
                    OnPropertyChanged("Count");
                }
            } 
        }

        public async void ToRecipePage()
        {
            Preferences.Set("current_product", _product.Id);
            await Application.Current.MainPage.Navigation.PushAsync(new SuggestedDishPage());
        }

        public void IncCount()
        {
            if (_count < _product.CountInStorage)
                Count = (_count + 1).ToString();
        }

        public void DecCount()
        {
            if (_count > 1)
                Count = (_count - 1).ToString();
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
