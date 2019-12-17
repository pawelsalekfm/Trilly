using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Trilly.ViewModels.Category;
using Trilly.ViewModels.Product;

namespace CatalogApi.Models.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region JsonMappers

            CreateMap<JsonCategoryImportVM, Category>()
                .ForMember(dest => dest.ExternalId, o => o.MapFrom(src => src.XlId))
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Slug, o => o.MapFrom(src => src.Slug))
                .ForMember(dest => dest.ProductCounter, o => o.MapFrom(src => src.ProductCounter))
                .ForMember(dest => dest.ExternalId, o => o.MapFrom(src => src.XlId))
                .ForMember(dest => dest.Id, o => o.Ignore());

            CreateMap<JsonProductImportVM, Product>()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.ExternalId, o => o.MapFrom(src => src.XlId))
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Slug, o => o.MapFrom(src => src.Slug))
                .ForMember(dest => dest.MarketingDescriptionHTML, o => o.MapFrom(src => src.MarketingDescriptionHTML))
                .ForMember(dest => dest.MarketingDescriptionSmall, o => o.MapFrom(src => src.MarketingDescriptionSmall))
                .ForMember(dest => dest.EanCode, o => o.MapFrom(src => src.EanCode))
                .ForMember(dest => dest.StockQuantity, o => o.MapFrom(src => src.OnlineStockQuantity))
                .ForMember(dest => dest.Price, o => o.MapFrom(src => src.PPriceGross))
                .ForMember(dest => dest.PricePromotion, o => o.MapFrom(src => src.PPricePromotion))
                .ForMember(dest => dest.Novelty, o => o.MapFrom(src => src.Novelty))
                .ForMember(dest => dest.Imported, o => o.MapFrom(src => src.Imported));

            CreateMap<JsonProductAttributeImportVm, ProductAttribute>()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, o => o.MapFrom(src => src.Value));

            #endregion

            #region ProductQueryMappers

            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Slug, o => o.MapFrom(src => src.Slug))
                .ForMember(dest => dest.Novelty, o => o.MapFrom(src => src.Novelty))
                .ForMember(dest => dest.EanCode, o => o.MapFrom(src => src.EanCode))
                .ForMember(dest => dest.Price, o => o.MapFrom(src => src.Price))
                .ForMember(dest => dest.PricePromotion, o => o.MapFrom(src => src.PricePromotion))
                .ForMember(dest => dest.ImageUrl, o => o.MapFrom(src => src.ImageUrl));

            #endregion
        }
    }
}
