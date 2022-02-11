using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Concrete.BaseRepositories;
using DataAccessLayer.Repositories.Interface.EntityTypeRepositories;
using EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete.EntityTypeRepositories
{
   public class AboutRepository : Repository<About>, IAboutRepository  // => BaseRepository<About> tipinde kalıtım aldık. Daha sonra inject edeceğimiz IAboutRepository tanımladık. Bunu yapmamızın amacı DIP prensibine uymamız. Sınıfları olabildiğince birbirinden bağımsız hale getirmek.
    {
        public AboutRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { } //=> Database bağlantısını yaptık.
    }
}
