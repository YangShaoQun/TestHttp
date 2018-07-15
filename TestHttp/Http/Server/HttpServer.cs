using System;
using System.Collections.Generic;
using MyService;

namespace MyServer
{
    public class HttpServer : Server
    {
        protected override List<IService> StartService(Server server)
        {
            List<IService> services = new List<IService>();
            services.Add(new HttpService());
            return services;
        }
    }
}
