using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using DataMappingService.Converters;
using DataMappingService.Interfaces;
using DataMappingService.Models;
using DataMappingService.Services;

namespace DataMappingService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(c =>
            {
                var builder = new ContainerBuilder();
                builder.RegisterApiControllers(typeof(WebApiApplication).Assembly)
                    .PropertiesAutowired();
                builder.RegisterType<CarDataConverter>().As<IConverter<CarData>>();
                builder.RegisterType<MappingService>().As<IService<CarData>>();
                var container = builder.Build();
                c.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            });
        }

        /// <summary>
        /// Глобальная обработка ошибок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
#if DEBUG
            Debugger.Break();
#endif

            Exception ex = Server.GetLastError();
            //NLogLogger logger = new NLogLogger();
            //logger.Error("Ошибка на сервере", ex);
        }
    }
}
