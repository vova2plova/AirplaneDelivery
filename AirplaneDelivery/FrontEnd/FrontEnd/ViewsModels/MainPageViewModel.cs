using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.ViewsModels
{
    class MainPageViewModel
    {
        const string url = "http://10.0.2.2:5000/Product/";
        private HttpClient GetCLient()
        {
            HttpClient client = new HttpClient(GetInsecureHandler());
            return client;
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

        private ObservableCollection<Product> _product;
        public ObservableCollection<Product> Product
        {
            get { return _product; }
            set { _product = value; }
        }
        public async Task LoadData()
        {
            var client = new HttpClient(GetInsecureHandler());
            var result = await client.GetAsync(url+ "GetAllProducts").ConfigureAwait(false);
            var json = await result.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
            Product = products;
        }

    }
}
