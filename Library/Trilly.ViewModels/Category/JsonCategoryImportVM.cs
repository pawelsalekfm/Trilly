using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Category
{
    public class JsonCategoryImportVM
    {
        public int Id { get; set; }
        public int XlId { get; set; }
        public int? WsId { get; set; }
        public int ChannelId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int CategoryOrder { get; set; }
        public byte[] BackgroundContent { get; set; }
        public byte[] IconContent { get; set; }
        public string ColorHex { get; set; }
        public string CircleColorHex { get; set; }
        public int? Position { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }

        public string ImageUrl { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }

        public bool Imported { get; set; }
        public bool EnableCompatibility { get; set; }
        public int ProductCounter { get; set; }
    }
}
