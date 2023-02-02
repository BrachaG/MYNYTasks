using AutoMapper;
using Entities;
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

            //var type = typeof(IEntity);
            //var types = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(s => s.GetTypes())
            //    .Where(p => type.IsAssignableFrom(p));
            //foreach (var item in types)
            //{ 
            //    var a = CreateMap(typeof(DataRow), item); 
            //       PropertyInfo[] properties = item.GetProperties();
            //    foreach (PropertyInfo property in properties)
            //    {
            //        a.ForMember(dest => dest.property.name,
            //        opt => opt.MapFrom(src => src[property.Name])
            //            );
            //         if (Table.Columns.Contains(property.Name))
            //        {
            //            object value = row[property.Name];
            //            property.SetValue(entity, value);
            //        }

            //    }

            //}
           
        }
    }
}
