using AutoMapper;
using PromotorApi.Model;
using System;
using System.ComponentModel;
using EnumsNET;

namespace PromotorApi.Mappings.Converters
{
    public class SlideShowStatusTypeConverter : IValueConverter<int, string>
    {
        public string Convert(int sourceMember, ResolutionContext context)
        {
            var statusEnum = Enum.Parse<SlideShowStatusEnum>(System.Convert.ToString(sourceMember));

            if (!statusEnum.IsValid())
                return string.Empty;

            return statusEnum.GetMember().Attributes.Get<DescriptionAttribute>().Description;
        }
    }
}
