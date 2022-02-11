using DataAccessLayer.Repositories.Interface.BaseRepositories;
using EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interface.EntityTypeRepositories
{
    public interface IAwardRepository:IBaseRepositories<Award>
    {
    }
}
