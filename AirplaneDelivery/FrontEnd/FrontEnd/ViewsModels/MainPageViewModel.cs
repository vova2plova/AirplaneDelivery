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
using System.Linq;

namespace FrontEnd.ViewsModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            MenuList = GetMenus();
        }

        public static ObservableCollection<Category> Categories;
        public static List<Product> Products;
        public static List<Product> AllProducts;
        public static StackLayout stack;
        public static Category OldCategory;

        private Command<int> _selectMenuCommand;
        private ObservableCollection<Menu> menuList;
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }
        public Command<Category> SelectCategoryCommand => new Command<Category>(Category =>
        {
            bool clear = false;
            stack.Children.Clear();
            if (AllProducts != null)
            {
                Category.isChoosen = Color.FromHex("#00C2FF");
                Categories[Categories.IndexOf(Category)] = Category;
                Products = AllProducts.Where(p => p.CategoryProduct.Id == Category.Id).ToList();
                if (OldCategory != null)
                {
                    OldCategory.isChoosen = Color.White;
                    Categories[Categories.IndexOf(OldCategory)] = OldCategory;
                    if (OldCategory.Id == Category.Id)
                    {
                        OldCategory = null;
                        Products = AllProducts;
                        clear = true;
                    }
                }
                if (clear == false)
                    OldCategory = Category;
                DrawCollection(stack, Products);
            }        
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

        public void SearchBarTextChanged(StackLayout stack,string text)
        {
            if (OldCategory != null)
            {
                OldCategory.isChoosen = Color.White;
                Categories[Categories.IndexOf(OldCategory)] = OldCategory;
                OldCategory = null;
            }
            stack.Children.Clear();
            if (AllProducts != null)
                Products = AllProducts.Where(x => x.Name.ToLower().Contains(text.ToLower())).ToList();
            if (text == "")
                Products = AllProducts;
            DrawCollection(stack, Products);
        }

        public static double GetGridContainerHeight(double itemCount, double columnCount, int rowHeight) 
        { 
            var rowCount = Math.Ceiling(itemCount / columnCount);
            return rowCount * rowHeight; 
        }
        public async void LoadProducts(StackLayout ProductList)
        {
            var response = await MainService.ProductService.GetAllProducts();
            if (response.IsSuccessStatusCode)
            {
                stack = ProductList;
                Products = new List<Product>(response.Content);
                AllProducts = new List<Product>(Products);
                DrawCollection(stack, Products);
            }
        }

        public async void LoadCategory(CollectionView CategoryList)
        {
            var response = await MainService.CategoryService.GetCategories();
            if (response.IsSuccessStatusCode)
            {
                Categories = new ObservableCollection<Category>(response.Content);
                CategoryList.ItemsSource = Categories;
                foreach (var category in Categories)
                    category.isChoosen = Color.White;
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

        private void DrawCollection(StackLayout MainStack, List<Product> products)
        {
            for (var i = 0; i < products.Count; i += 2)
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
                    Source = ImageSource.FromUri(products[i].Image),
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
                    Text = products[i].Name,
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
                    Text = products[i].Price.ToString() + " ₽",
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
                    CommandParameter = products[i]
                };

                leftFrame.GestureRecognizers.Add(leftTap);

                stack.Children.Add(leftFrame);

                if (i + 1 != Products.Count)
                {
                    rightName.Text = products[i + 1].Name;
                    rightPrice.Text = products[i + 1].Price.ToString() + " ₽";
                    rightImage.Source = ImageSource.FromUri(products[i + 1].Image);
                    rightStack.Children.Add(rightImage);
                    rightStack.Children.Add(rightName);
                    rightStack.Children.Add(rightPrice);

                    var rightTap = new TapGestureRecognizer()
                    {
                        Command = SelectProductCommand,
                        CommandParameter = products[i + 1]
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
