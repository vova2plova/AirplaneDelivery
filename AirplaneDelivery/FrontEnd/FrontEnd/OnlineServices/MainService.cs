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
        }

        public static IUserService UserService { get; set; }

    }

}

