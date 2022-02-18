using DAL.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.ViewsModels
{
    class LoginPageViewModel
    {
        const string url = "http://0e4a-176-52-96-105.ngrok.io/User/";
        private HttpClient GetCLient()
        {
            HttpClient client = new HttpClient();
            return client;
        }
        public  async Task<User> SignInUser(string login, string password)
        {
            HttpClient client = GetCLient();
            var result = await client.GetAsync(url + $"SignIn?login={login}&password={password}");
            var json = await result.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(json);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
