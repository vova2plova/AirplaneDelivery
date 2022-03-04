using DAL.Models;
using FrontEnd.OnlineServices;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using FrontEnd.Views;
using Acr.UserDialogs;

namespace FrontEnd.ViewsModels
{
    class LoginPageViewModel
    {


        private LoginPage loginPage;
     
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public ICommand EnterCommand => new Command<User>(async value =>
        {

            Console.WriteLine(value.Name, value.Password);
            var response = await MainService.UserService.SignIn(value.Name,value.Password);
            if (response.IsSuccessStatusCode)
            {

                loginPage.ok();
            }
            else
            {
                UserDialogs.Instance.Toast("Введены неверные данные!");
            }
        });

        public ICommand EnterCommand2 => new Command<User>(async value =>
        {


            var response = await MainService.UserService.SignUp(value);
            if (response.IsSuccessStatusCode)
            {

                UserDialogs.Instance.Toast("Вы успешно зарегестрировались!");
                loginPage.CloseSignUp();
            }
            else
            {
                UserDialogs.Instance.Toast("Пользовтаель с таким номером телефона уже зарегистрирован!");
            }
        });
    }

}
