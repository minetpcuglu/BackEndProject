using DataAccessLayer.Mapping.Interface;
using EntityLayer.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mapping.Concrete
{
   public class HobbyMap:IBaseMap<Hobby>
    {
        public override void Configure(EntityTypeBuilder<Hobby> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MyHobby).HasMaxLength(250).IsRequired(true);

            base.Configure(builder);

        }
    }
}
