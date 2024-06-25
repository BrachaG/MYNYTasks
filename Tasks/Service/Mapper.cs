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
                    if (value == DBNull.Value)
                    {
                        property.SetValue(entity, null);
                    }
                    else
                    {
                        property.SetValue(entity, value);
                    }
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
            CreateMap<DataRow, ResultsForSurvey>()
             .ConvertUsing(row => MapDataRowToEntity<ResultsForSurvey>(row));
            CreateMap<DataRow, ResultsForSurveyStudent>()
             .ConvertUsing(row => MapDataRowToEntity<ResultsForSurveyStudent>(row));
            CreateMap<DataRow, Question>()
             .ConvertUsing(row => MapDataRowToEntity<Question>(row));
            CreateMap<DataRow, Answer>()
             .ConvertUsing(row => MapDataRowToEntity<Answer>(row));
            CreateMap<DataRow, Options>()
             .ConvertUsing(row => MapDataRowToEntity<Options>(row));
            CreateMap<DataRow, TaskModel>()
             .ConvertUsing(row => MapDataRowToEntity<TaskModel>(row));
            CreateMap<DataRow, Target>()
             .ConvertUsing(row => MapDataRowToEntity<Target>(row));
            CreateMap<DataRow, TaskType>()
             .ConvertUsing(row => MapDataRowToEntity<TaskType>(row));
            CreateMap<DataRow, TargetType>()
             .ConvertUsing(row => MapDataRowToEntity<TargetType>(row));
            CreateMap<DataRow, TargetStatus>()
           .ConvertUsing(row => MapDataRowToEntity<TargetStatus>(row));
            CreateMap<DataRow, Branch>()
          .ConvertUsing(row => MapDataRowToEntity<Branch>(row));
            CreateMap<DataRow, BranchGroup>()
          .ConvertUsing(row => MapDataRowToEntity<BranchGroup>(row));
            CreateMap<DataRow, StudentForTask>()
    .ForMember(dest => dest.iPersonId, opt => opt.MapFrom(src => src.Field<int>("iPersonId")))
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Field<string>("FullName")))
    .ForMember(dest => dest.iBranchId, opt => opt.MapFrom(src => src.Field<int>("iBranchId")))
    .ForMember(dest => dest.nvBranchName, opt => opt.MapFrom(src => src.Field<string>("nvBranchName")));
        }
    }
}
