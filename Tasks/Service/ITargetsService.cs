using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITargetsService
    {
         string GetTargetsByUserId(int UserId,int Status);
    }
}
