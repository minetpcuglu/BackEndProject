

using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace BackEndProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            //NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)

              .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // projenin ya�am s�relerini autofac ile belle�e kaydetme 
              .ConfigureContainer<ContainerBuilder>(builder =>
              {
                  builder.RegisterModule(new RepositoriesModule());
              })
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder.UseStartup<Startup>();
                  });
    }
}
