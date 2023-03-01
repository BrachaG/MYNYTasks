using AutoMapper;
using Entities;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection;

namespace Service
{
    public class Mapper : Profile
    {
        public static T MapDataRowToEntity<T>(DataRow row) where T : new()
        {
            T entity = new T();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    object value = row[property.Name];
                    property.SetValue(entity, value);
                }
            }
            return entity;
        }
        public Mapper()
        {
            CreateMap<DataRow, User>()
             .ConvertUsing(row => MapDataRowToEntity<User>(row));
            CreateMap<DataRow, CodeTable>()
             .ConvertUsing(row => MapDataRowToEntity<CodeTable>(row));
            CreateMap<DataRow, Survey>()
             .ConvertUsing(row => MapDataRowToEntity<Survey>(row));
        }
    }
}
