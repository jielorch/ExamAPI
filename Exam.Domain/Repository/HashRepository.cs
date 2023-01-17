using Exam.Domain.Interface;
using Exam.Entities;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain.Repository
{
    public class HashRepository : RepositoryBase<Hash>, IHashRepository
    {
        public HashRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
