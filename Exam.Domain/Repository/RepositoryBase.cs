using Exam.Domain.Interface;
using Exam.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbContext;
        //private readonly IDbConnection _dbConnection;

        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_dbConnection = _appDbContext.Connection;
        }

        public virtual void Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _appDbContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public virtual void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
        }

    }
}
