using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FrontEnd.OnlineServices
{
    class BaseDataService<T>
    {
        public T InstanceInterface
        {
            get
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri(AppSettings.BaseURI)
                };
                return RestService.For<T>(client);
            }
        }

    }
}
