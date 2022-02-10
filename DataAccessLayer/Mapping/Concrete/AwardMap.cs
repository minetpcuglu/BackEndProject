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
   public class AwardMap : IBaseMap<Award>
    {
        public override void Configure(EntityTypeBuilder<Award> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AwardName).HasMaxLength(100).IsRequired(true);

            base.Configure(builder);

        }
    }
}
