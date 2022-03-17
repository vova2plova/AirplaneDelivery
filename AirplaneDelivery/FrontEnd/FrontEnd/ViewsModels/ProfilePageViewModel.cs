using Acr.UserDialogs;
using DAL.Models;
using FrontEnd.OnlineServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FrontEnd.ViewsModels
{
    class ProfilePageViewModel
    {
        public ObservableCollection<Cart> Carts { get; set; }
        public ICommand SaveCommand => new Command<User>(async value =>
        {
            var response = await MainService.UserService.UpdateData(value);
            if (response.IsSuccessStatusCode)
            {
                UserDialogs.Instance.Toast("Данные успешно изменены!");
            }
        });

        public string SumPrice(Cart cart)
        {
            int sum = 0;
            foreach (var item in cart.Spots)
            {
                sum += (item.Count * item.Products.Price);
            }
            return sum.ToString() + " ₽";

        }

        public async void LoadHistory(StackLayout List)
        {
            var result = await MainService.CartService.GetHistoryOrder(Preferences.Get("user_id",0));
            if (result.IsSuccessStatusCode)
            {
                List<Cart> carts = new List<Cart>(result.Content);
                foreach (var cart in carts)
                {
                    cart.Sum = SumPrice(cart);
                }
                Carts = new ObservableCollection<Cart>(carts);
                BindableLayout.SetItemsSource(List, Carts);
            }
        }
    }
       
    }

