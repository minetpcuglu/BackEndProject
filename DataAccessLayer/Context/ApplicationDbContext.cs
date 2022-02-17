using DataAccessLayer.Mapping.Concrete;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
  public class ApplicationDbContext: IdentityDbContext<AppUser,AppRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } // =>  "DB bağlantısını concructor method ile oluşturuldu."

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles  { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }

        // Şimdi yaptığımız Map'leme işlemini override edeceğiz.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AboutMap());
            builder.ApplyConfiguration(new EducationMap());
            builder.ApplyConfiguration(new HobbyMap());
            builder.ApplyConfiguration(new AwardMap());
       
            base.OnModelCreating(builder);
        }

    }
}
