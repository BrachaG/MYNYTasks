using System.Data;

namespace Service
{
    public interface IObjectGenerator<T> where T : new()
    {
        public T GeneratFromDataRow(DataRow dr);
        public List<T> GeneratListFromDataRowCollection(DataRowCollection collection);
    }
}
