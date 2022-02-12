using DataAccessLayer.Repositories.Interface.EntityTypeRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWorks.Interface
{
   public interface IUnitOfWork : IAsyncDisposable
    {
        //IAboutRepository AboutRepository { get; }
        IEducationRepository EducationRepository { get; }
        IHobbyRepository HobbyRepository { get; }
        //IAwardRepository AwardRepository { get; }
        Task Commit();  // => Başarılı bir işlemin sonucunda çalıştırılır. İşlemin başalamasından itibaren tüm değişikliklerin veri tabanına uygulanmasını temin eder.

        //Task ExecuteSqlRaw(string sql, params object[] parameters); //Mevcut sql sorgularımızı doğrudan veritabanında yürütmek için kullanılan bir methoddur.

    }
}
