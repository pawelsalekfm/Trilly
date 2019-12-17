using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PromotorApi.Model;
using Trilly.ViewModels.Promotor;

namespace PromotorApi.Mappings
{
    public class SlideShowToVmMappingProfile : Profile
    {
        public SlideShowToVmMappingProfile()
        {
            CreateMap<SlideShow, SlideShowVm>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.ValidFrom, o => o.MapFrom(src => src.ValidFrom))
                .ForMember(dest => dest.ValidTo, o => o.MapFrom(src => src.ValidTo))
                .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreationDate, o => o.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastUpdateDate, o => o.MapFrom(src => src.LastUpdateDate))
                .ForMember(dest => dest.Slides, o => o.MapFrom(src => src.Slides));
                //.ForMember(dest => dest.Slides, o => o.Ignore());
        }
    }
}
