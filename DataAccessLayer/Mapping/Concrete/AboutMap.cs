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
   public class AboutMap : IBaseMap<About>
    {
        public override void Configure(EntityTypeBuilder<About> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(70).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(20).IsRequired(true);
            builder.Property(x => x.Mail).HasMaxLength(70).IsRequired(true);
            builder.Property(x => x.TelephoneNumber).HasMaxLength(11).IsRequired(false);
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Adress).IsRequired(false);
            builder.Property(x => x.AgainPassword).IsRequired(false);
            builder.Property(x => x.ImagePath).HasMaxLength(75).IsRequired(false);
            
  

            base.Configure(builder);

        }

    }
}
