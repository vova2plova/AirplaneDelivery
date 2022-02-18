using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FrontEnd.ViewsModels
{
    class MainPageViewModel
    {
        const string url = "http://10.0.2.2:5000/User/";
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



    }
}
