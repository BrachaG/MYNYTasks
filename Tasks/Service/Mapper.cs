using AutoMapper;
using Entities;
using System.Data;

namespace Service
{
    public class Mapper: Profile
    {
        
        public Mapper()
        {
            CreateMap<DataRow, User>();
            CreateMap<DataRow, CodeTable>();
        }
    }
}