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
   public class AwardRepository : Repository<Award>, IAwardRepository
    {
        public AwardRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }  
    }
}
