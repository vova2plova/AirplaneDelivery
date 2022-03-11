using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FrontEnd.ViewsModels
{
    internal class DetailPageViewModel : INotifyPropertyChanged
    {
        Product _product;

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
