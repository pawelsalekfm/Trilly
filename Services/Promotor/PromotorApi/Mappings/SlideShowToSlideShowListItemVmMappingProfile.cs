using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnumsNET;
using PromotorApi.Application.SlideShow.Commands.CreateSlideShow;
using PromotorApi.Mappings.Converters;
using PromotorApi.Model;
using Trilly.ViewModels.Promotor;

namespace PromotorApi.Mappings
{
    public class SlideShowToSlideShowListItemVmMappingProfile : Profile
    {
        public SlideShowToSlideShowListItemVmMappingProfile()
        {
            CreateMap<SlideShow, SlideShowListItemVm>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status))
                .ForMember(dest => dest.ValidFrom, o => o.MapFrom(src => src.ValidFrom))
                .ForMember(dest => dest.ValidTo, o => o.MapFrom(src => src.ValidTo))
                .ForMember(dest => dest.CreationDate, o => o.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.LastUpdateDate, o => o.MapFrom(src => src.LastUpdateDate))
                .ForMember(dest => dest.StatusString, o => o.ConvertUsing(new SlideShowStatusTypeConverter(), src=>src.Status));
        }
    }
}
