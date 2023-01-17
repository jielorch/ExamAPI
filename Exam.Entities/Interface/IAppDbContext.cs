using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Interface
{
    public interface IAppDbContext
    {
        public IDbConnection Connection { get; }
        public DbSet<Hash>? Hashes { get; set; }
    }
}
