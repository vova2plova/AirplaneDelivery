﻿using DAL.Models;
using FrontEnd.OnlineServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FrontEnd.ViewsModels
{
    internal class CartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Spot> Spots { get; set; } = new ObservableCollection<Spot>();
        private User _user;

        public event PropertyChangedEventHandler PropertyChanged;
        private int _SumPrice;
        private string _Address = "";
        public async void LoadCart(StackLayout Spot_List)
        {
            var result = await MainService.CartService.GetUserCart(Preferences.Get("user_id", 0));
            if (result.IsSuccessStatusCode)
            {
                Spots = new ObservableCollection<Spot>(result.Content);
                BindableLayout.SetItemsSource(Spot_List, Spots);
                LoadUser();
                GetSumPrice();
            }
        }

        public void GetSumPrice()
        {
            var sum = 0;
            foreach (var spot in Spots)
                sum += (spot.Products.Price * spot.Count);
            Sum = sum.ToString();
        }

        public async void LoadUser()
        {
            var result = await MainService.UserService.GetUserById(Preferences.Get("user_id", 0));
            if (result.IsSuccessStatusCode)
            {
                _user = result.Content;
                Address = _user.Address == null ? "" : _user.Address;
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
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

        public string Address
        {
            get => _Address;
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public Command<Spot> IncSpotCount => new Command<Spot>(Spot =>
        {
            if (Spot.Count < Spot.Products.CountInStorage)
                Spot.Count++;
        });

        public Command<Spot> DecSpotCount => new Command<Spot>(Spot =>
        {
            if (Spot.Count >= 0)
                Spot.Count--;
            else
                Spots.Remove(Spot);
            OnPropertyChanged("Count");
            GetSumPrice();
        });
    }
}
