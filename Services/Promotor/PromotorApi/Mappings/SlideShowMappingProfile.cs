using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PromotorApi.Application.SlideShow.Commands.CreateSlideShow;
using PromotorApi.Model;

namespace PromotorApi.Mappings
{
    public class SlideShowMappingProfile : Profile
    {
        public SlideShowMappingProfile()
        {
            CreateMap<CreateSlideShowCommand, SlideShow>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.ValidFrom, o => o.MapFrom(src => src.ValidFrom));
        }       
    }
}
