using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.UserService
{
    class UserService : BaseDataService<IUserService>, IUserService
    {
        public async Task<ApiResponse<User>> SignIn(string login, string password)
        {
           return await InstanceInterface.SignIn(login, password);
        }

        public async Task<ApiResponse<User>> SignUp(User newUser)
        {
            return await InstanceInterface.SignUp(newUser);
        }

    }

}

