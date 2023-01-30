using AutoMapper;
using Entities;
using System.Data;

namespace Service
{
    public class Mapper : Profile
    {

        public Mapper()
        {


            CreateMap<DataRow, CodeTable>()
                          .ForMember(
                        dest => dest.iId,
                        opt => opt.MapFrom(src => src["iId"])
                    )
                    .ForMember(
                        dest => dest.nvName,
                        opt => opt.MapFrom(src => src["nvName"])
                    );
            CreateMap<DataRow, User>()
                .ForMember(
                    dest => dest.iUserId,
                    opt => opt.MapFrom(src => src["iUserId"])
                )
                .ForMember(
                    dest => dest.nvUserName,
                    opt => opt.MapFrom(src => src["nvUserName"])
                )
                .ForMember(
                    dest => dest.nvPassword,
                    opt => opt.MapFrom(src => src["nvPassword"])
                )
               .ForMember(
                    dest => dest.iUserStatus,
                    opt => opt.MapFrom(src => src["iUserStatus"])
                )
                .ForMember(
                    dest => dest.iOrganizationId,
                    opt => opt.MapFrom(src => src["iOrganizationId"])
                )
               .ForMember(
                    dest => dest.iPermissionLevelId,
                    opt => opt.MapFrom(src => src["iPermissionLevelId"])
                )
               .ForMember(
                    dest => dest.iContinueContactPermissionId,
                    opt => opt.MapFrom(src => src["iContinueContactPermissionId"])
                )
               .ForMember(
                    dest => dest.iWorkerId,
                    opt => opt.MapFrom(src => src["iWorkerId"])
                )
               .ForMember(
                    dest => dest.iBranchId,
                    opt => opt.MapFrom(src => src["iBranchId"])
                )
               .ForMember(
                    dest => dest.dtValidityDate,
                    opt => opt.MapFrom(src => src["dtValidityDate"])
                )
               .ForMember(
                    dest => dest.dtLatestentering,
                    opt => opt.MapFrom(src => src["dtLatestentering"])
                )
                .ForMember(
                    dest => dest.isConcentratedReport,
                    opt => opt.MapFrom(src => src["isConcentratedReport"])
                )
                 .ForMember(
                    dest => dest.nvUserMail,
                    opt => opt.MapFrom(src => src["nvUserMail"])
                )
                  .ForMember(
                    dest => dest.nvUserPhone,
                    opt => opt.MapFrom(src => src["nvUserPhone"])
                )
                   .ForMember(
                    dest => dest.iActivityPermissionId,
                    opt => opt.MapFrom(src => src["iActivityPermissionId"])
                );

        }

    }

}
