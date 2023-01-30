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
       
        public T GeneratFromDataRow(DataRow dr)
        {
            T obj = new T();
            obj = _mapper.Map<DataRow, T>(dr);
            return obj;
        }
        public List<T> GeneratListFromDataRowCollection(DataRowCollection collection)
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