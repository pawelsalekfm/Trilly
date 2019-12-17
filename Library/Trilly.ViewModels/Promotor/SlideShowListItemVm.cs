using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Promotor
{
    public class SlideShowListItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
