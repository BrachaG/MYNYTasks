using AutoMapper;
using System.Data;


namespace Service
{
    public class ObjectGenerator<T> : IObjectGenerator<T> where T : new()
    {
        IMapper _mapper;
        public ObjectGenerator(IMapper mapper)
        {

            _mapper = mapper;
        }
        //return T object full data from DataRow
        //Good for select queries


        public T GeneratFromDataRow(DataRow dr)
        {


            T obj = new T();
            obj = _mapper.Map<DataRow, T>(dr);
            return obj;
        }
        //get DataSet, call GeneratFromDataRow() in repeat and return list T
        public List<T> GeneratListFromDataTable(DataTable dt)
        {
            List<T> rows = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {

                T obj = GeneratFromDataRow(dr);
                rows.Add(obj);
            }
            return rows;


        }





    }
}