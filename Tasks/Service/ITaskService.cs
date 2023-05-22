using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITaskService
    {
       Task<List<Tasks>> Get();
       void Add(Tasks task, string iUserId);
       

    }
}
