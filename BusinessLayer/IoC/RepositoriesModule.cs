

using Autofac;
using BusinessLayer.Services.Concrete;
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
            builder.RegisterType<EducationRepository>().As<IEducationRepository>().InstancePerLifetimeScope(); 
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            
        }
    }
}
