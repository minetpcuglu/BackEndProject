

using Autofac;
using BusinessLayer.Services.Concrete;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Repositories.Concrete.EntityTypeRepositories;
using DataAccessLayer.Repositories.Interface.EntityTypeRepositories;
using DataAccessLayer.UnitOfWorks.Concrete;
using DataAccessLayer.UnitOfWorks.Interface;

namespace BusinessLayer.IoC
{
    public class RepositoriesModule : Module //UnitOfWork için bagımlılıklardan kurtulmak amacıyla IoC Containerlardan yardım almak 
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HobbyRepository>().As<IHobbyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HobbyService>().As<IHobbyService>().InstancePerLifetimeScope(); //senden ıhobbyservice istenilirse arka planda hobby service verilir //singleton içerisinde data tutulmuyorsa kullanılır
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EducationRepository>().As<IEducationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();


        }
    }
}
