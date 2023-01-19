using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class xs
    {
        public List<T> MapDataTableToObjects<T>(DataTable dataTable)
{
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataRow, T>()
                    .ForAllMembers(opt => opt.MapFrom(src => src));
            });
            var mapper = config.CreateMapper();
            return dataTable.AsEnumerable().Select(mapper.Map<T>).ToList();
        }



        DataTable dataTable = new DataTable();
        List<Customer> customers = MapDataTableToObjects<Customer>(dataTable);
  
using AutoMapper.Data;

DataTable dataTable = new DataTable();
    var customers = dataTable.AsEnumerable()
        .Map<Customer>()
        .ToList();

}
}
