using Acr.UserDialogs;
using DAL.Models;
using FrontEnd.OnlineServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FrontEnd.ViewsModels
{
    class ProfilePageViewModel
    {
        
       
        public ICommand SaveCommand => new Command<User>(async value =>
        {
            var response = await MainService.UserService.UpdateData(value);
            if (response.IsSuccessStatusCode)
            {
                UserDialogs.Instance.Toast("Данные успешно изменены!");
            }
           
        }
       
        );
    }
       
    }

