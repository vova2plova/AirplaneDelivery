using DAL.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.ViewsModels
{
    class LoginPageViewModel
    {
        const string url = "http://192.168.1.5:5000/User/";
        private HttpClient GetCLient()
        {
            HttpClient client = new HttpClient(GetInsecureHandler());
            return client;
        }
        public  async Task<User> SignInUser(string login, string password)
        {
            HttpClient client = GetCLient();
            var result = await client.GetAsync(url + $"SignIn?login={login}&password={password}").ConfigureAwait(false);
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
    }
}
