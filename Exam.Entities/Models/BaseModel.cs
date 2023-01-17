using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
        public virtual DateTime? Date { get; set; }
    }
}
