using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    public abstract class ObjectGenerator<T> where T : new()
    {
        static IMapper _mapper;
        public  ObjectGenerator(IMapper mapper)
        {

            _mapper = mapper;
        }
        //return T object full data from DataRow
        //Good for select queries
       

        public static T GeneratFromDataRow(DataRow dr)
        {
            T obj = _mapper.Map<DataRow,T>(dr);
            return obj;
        }
        //get DataSet, call GeneratFromDataRow() in repeat and return list T
        public static List<T> GeneratListFromDataRowCollection(DataRowCollection collection)
        {
            List<T> rows = new List<T>();
            foreach (DataRow dr in collection)
            {

                T obj = GeneratFromDataRow(dr);
                rows.Add(obj);
            }
            return rows;


        }
    }
}

