using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IObjectGenerator<T>
    {
        public T GeneratFromDataRow(DataRow dr);
        public List<T> GeneratListFromDataRowCollection(DataRowCollection collection);
    }
}
