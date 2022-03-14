using FrontEnd.OnlineServices.CartService;
using FrontEnd.OnlineServices.CategoryService;
using FrontEnd.OnlineServices.ProductService;
using FrontEnd.OnlineServices.UserService;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrontEnd.OnlineServices
{
    class MainService
    {
        public static void Init()
        {
            UserService = new UserService.UserService();
            CategoryService = new CategoryService.CategoryService();
            ProductService = new ProductService.ProductService();
            CartService = new CartService.CartService();
        }

        public static IUserService UserService { get; set; }
        public static ICategoryService CategoryService { get; set; }
        public static IProductService ProductService { get; set; }
        public static ICartService CartService { get; set; }
    }

}

