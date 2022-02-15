using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Concrete.EntityTypeRepositories;
using DataAccessLayer.Repositories.Interface.EntityTypeRepositories;
using DataAccessLayer.UnitOfWorks.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWorks.Concrete
{
    public class UnitOfWork : IUnitOfWork // => IUnitOfWork'den implement yolu ile gövdelendireceğim methodlar alındı
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction _transation;

        public UnitOfWork(ApplicationDbContext db)
        {
           this._db =db ?? throw new ArgumentNullException("Database Can Not To Be Null..!");
        }
        //=> ??= Karar mekanizmasını başlattık. Bu karar mekanizması ya bize db bağlantısını verecek ya da ArgumentNullException ile hata mesajımı gönderecektir.

        //private IAboutRepository _aboutRepository;
        //public IAboutRepository AboutRepository
        //{
        //    get
        //    {
        //        if (_aboutRepository == null) _aboutRepository = new AboutRepository(_db);
        //        return _aboutRepository;
        //    }
        //}

        //private IAwardRepository _awardRepository;
        //public IAwardRepository AwardRepository
        //{
        //    get
        //    {
        //        if (_awardRepository == null) _awardRepository = new AwardRepository(_db);
        //        return _awardRepository;
        //    }
        //}


        private IEducationRepository _educationRepository;
        public IEducationRepository EducationRepository
        {
            get
            {
                if (_educationRepository == null) _educationRepository = new EducationRepository(_db);
                return _educationRepository;
               
            }
        }

        private IHobbyRepository _hobbyRepository;
        public IHobbyRepository HobbyRepository
        {
            get
            {
                if (_hobbyRepository == null) _hobbyRepository = new HobbyRepository(_db);
                return _hobbyRepository;
            }
        }

        public async Task Commit() => await _db.SaveChangesAsync();


        public async Task<int> SaveChangesAsync()
        {
            var transaction = _transation ?? _db.Database.BeginTransaction();
            var count = 0;

            using (transaction)
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }

            return count;
        }

        private bool isDisposing = false;       //************sor //commit işlemi için sanırım  sor ?
        public async ValueTask DisposeAsync()
        {
            if (!isDisposing)
            {
                isDisposing = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);    // Nesnemizi tamamıyla temizlenmesini sağlayack.
            }
        }
        private async Task DisposeAsync(bool disposing)
        {
            if (disposing) await _db.DisposeAsync(); // => Üretilen db nesnemizi dispose ettik.
        }
    }
}
