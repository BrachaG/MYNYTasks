using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Data;


namespace Service
{
    public class ObjectGenerator<T> : IObjectGenerator<T> where T : new()
    {
        ILogger<ObjectGenerator<T>> _logger;
        IMapper _mapper;
        public ObjectGenerator(IMapper mapper, ILogger<ObjectGenerator<T>> logger)
        {
            _logger = logger;
            _mapper = mapper;
        }
        //return T object full data from DataRow
        //Good for select queries


        public T GeneratFromDataRow(DataRow dr)
        {
            _logger.LogDebug($"DataRow is: {dr}  In GeneratFromDataRow of ObjectGenerator");
            T obj = new T();
            obj = _mapper.Map<DataRow, T>(dr);
            _logger.LogDebug($"The object is: {obj} DataRow is: {dr}  In GeneratFromDataRow, After Mapping of ObjectGenerator");
            return obj;
        }
        //get DataSet, call GeneratFromDataRow() in repeat and return list T
        public List<T> GeneratListFromDataTable(DataTable dt)
        {
            _logger.LogDebug($"DataTable is: {dt}  In GeneratListFromDataTable of ObjectGenerator");
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