using Exam.Domain.Interface;
using Exam.Domain.Repository;
using Exam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _appDbContext;
        private IHashRepository? _hashRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; 
        }

        public IHashRepository HashRepo
        {
            get
            {
                _hashRepository ??= new HashRepository(_appDbContext); return _hashRepository;
            }
        }

        public void Dispose()
        {
            if(_appDbContext.Connection.State == System.Data.ConnectionState.Open)
            {
                _appDbContext.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
