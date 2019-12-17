using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PromotorApi.Model;
using Trilly.ViewModels.Promotor;

namespace PromotorApi.Mappings
{
    public class SlideShowItemToVmMappingProfile : Profile
    {
        public SlideShowItemToVmMappingProfile()
        {
            CreateMap<SlideShowItem, SlideShowItemVm>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Content, o => o.MapFrom(src => src.Content))
                .ForMember(dest => dest.Order, o => o.MapFrom(src => src.Order))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description));
        }
    }
}
