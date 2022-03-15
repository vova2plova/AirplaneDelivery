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
        public List<Product> Products;

        private Command<int> _selectMenuCommand;
        private ObservableCollection<Menu> menuList;
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }
        public Command<Category> SelectCategoryCommand => new Command<Category>(Category =>
        {

            var index = Category.Id;
       
            Application.Current.MainPage.DisplayAlert("Selected Plan", "название категории : " + Category.Title, "Ok");

        });

        public Command<Product> SelectProductCommand => new Command<Product>(async Product =>
        {
            using (UserDialogs.Instance.Loading("Страница загружается", null, null, true, MaskType.Black))
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(Product));
            }
        });


        private ObservableCollection<Menu> GetMenus()
        { 
            return new ObservableCollection<Menu>
            {
                new Menu {Id=1, Icon = "order.png", Name = "Мои данные"},
                new Menu {Id=2, Icon = "cart.png", Name = "Корзина"},
                new Menu{Id=3 ,Icon = "settings.png", Name = "Выйти из аккаунта"}
            };
        }

        public static double GetGridContainerHeight(double itemCount, double columnCount, int rowHeight) 
        { 
            var rowCount = Math.Ceiling(itemCount / columnCount);
            return rowCount * rowHeight; 
        }
        public async void LoadProducts(StackLayout ProdutList)
        {
            var response = await MainService.ProductService.GetAllProducts();
            if (response.IsSuccessStatusCode)
            {
                Products = new List<Product>(response.Content);
                DrawCollection(ProdutList);
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
                            Application.Current.MainPage.Navigation.PushAsync(new ProfilePage());
                            break;
                        case 2:
                            Application.Current.MainPage.Navigation.PushAsync(new CartPage());
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

        private void DrawCollection(StackLayout MainStack)
        {
            for (var i = 0; i < Products.Count; i += 2)
            {
                var stack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };


                var leftFrame = new Frame()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(10),
                    WidthRequest = 200,
                    CornerRadius = 15,
                    BackgroundColor = Color.FromHex("#F3F3F3")
                };
                var rightFrame = new Frame()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(10),
                    WidthRequest = 200,
                    CornerRadius = 15,
                    BackgroundColor = Color.FromHex("#F3F3F3")
                };

                var leftStack = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                var rightStack = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                var leftImage = new Image
                {
                    Source = ImageSource.FromUri(Products[i].Image),
                    HeightRequest = 100,
                    WidthRequest = 100
                };

                var rightImage = new Image
                {
                    HeightRequest = 100,
                    WidthRequest = 100,
                };

                var leftName = new Label()
                {
                    FontSize = 18,
                    Text = Products[i].Name,
                    TextColor = Color.Black
                };

                var rightName = new Label()
                {
                    FontSize = 18,
                    TextColor = Color.Black
                };

                var leftPrice = new Label()
                {
                    FontSize = 16,
                    Text = Products[i].Price.ToString() + " ₽",
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.EndAndExpand
                };
                var rightPrice = new Label()
                {
                    FontSize = 16,
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.EndAndExpand
                };

                leftStack.Children.Add(leftImage);
                leftStack.Children.Add(leftName);
                leftStack.Children.Add(leftPrice);
                leftFrame.Content = leftStack;

                var leftTap = new TapGestureRecognizer
                {
                    Command = SelectProductCommand,
                    CommandParameter = Products[i]
                };

                leftFrame.GestureRecognizers.Add(leftTap);

                stack.Children.Add(leftFrame);

                if (i + 1 != Products.Count)
                {
                    rightName.Text = Products[i + 1].Name;
                    rightPrice.Text = Products[i + 1].Price.ToString() + " ₽";
                    rightImage.Source = ImageSource.FromUri(Products[i + 1].Image);
                    rightStack.Children.Add(rightImage);
                    rightStack.Children.Add(rightName);
                    rightStack.Children.Add(rightPrice);

                    var rightTap = new TapGestureRecognizer()
                    {
                        Command = SelectProductCommand,
                        CommandParameter = Products[i + 1]
                    };

                    rightFrame.Content = rightStack;
                    rightFrame.GestureRecognizers.Add(rightTap);
                    stack.Children.Add(rightFrame);
                }
                else
                {
                    rightFrame.BorderColor = Color.White;
                }

                MainStack.Children.Add(stack);
            }
        }
    }
}
